using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;


[RequireComponent(typeof(Plant))]
[RequireComponent(typeof(PlantLevel))]
[RequireComponent(typeof(PlantCollisionConfig))]
[RequireComponent(typeof(PlantRegenerateSeeds))]
[AddComponentMenu(nameof(Carry))]
public class PlantController : Plant
{ 
    private Plant plantObject;

    [SerializeField]
    [Tooltip("Adicione aqui os prefabs de semente, muda e árvore respectivamente.")]
    private GameObject seedGameObject, seedPlantedGameObject, sproutGameObject, treeGameObject;

    [SerializeField]
    [Header("Status da planta")]
    [Tooltip("Altere aqui qual o status atual da planta durante o jogo para semente, muda ou árvore.")]
    private PlantStates plantStatus;

    [SerializeField]
    [Range(0, 100)]
    [Tooltip("Nível atual da água da planta, não precisa ser alterado, está no inspector para própositos de assistir a velocidade em que o nível de água sobe durante o jogo.")]
    private int plantWaterLevel;

    [HideInInspector]
    public int PlantWaterLevel
    {
        get {return plantWaterLevel; }
        set {
            plantWaterLevel = value;
            if (plantWaterLevel >= waterLevelMax)
            {
                OnMaxedLevel?.Invoke();
            }
        }
    }

    [HideInInspector]
    public UnityEvent OnMaxedLevel;

    [SerializeField]
    [Range(30, 100)]
    [Tooltip("Altere aqui qual o nível máximo de água da planta.")]
    private int waterLevelMax = 50;

    [SerializeField]
    [Range(1, 98)]
    [Tooltip("Nível de água necessário para a semente brotar para muda.")]
    private int sproutWaterLevel = 15;

    [SerializeField]
    [Range(2, 99)]
    [Tooltip("Nível de água necessário para a muda brotar para árvore")]
    private int treeWaterLevel = 30;

    private Vector3 originalPos;

    [Space(10)]
    [SerializeField]
    [Range(0,3)]
    [Tooltip("Altere aqui a margem de posicionamento vertical da planta.")]
    private float offsetPlantPositionY = 0.4f;

    #region Inicializa��o de objeto
    private void OnEnable()
    {
        AttributeGameObjects();
        SetInitialPlantState();
        AddTotalPlants();       
        CheckPlantState();

        TryGetComponent<PlantLevel>(out var plantLevel);
        plantLevel.plantEvent += CheckGrow;
    }

    private void OnDisable()
    {
        TryGetComponent<PlantLevel>(out var plantLevel);
        plantLevel.plantEvent -= CheckGrow;
    }

    private void OnValidate()
    {
        CheckPlantState();
    }

    public override void AttributeGameObjects()
    {
        SeedGameObject = seedGameObject;
        SproutGameObject = sproutGameObject;
        TreeGameObject = treeGameObject;
    }

    public override void SetInitialPlantState()
    {
        originalPos = gameObject.transform.position;
        plantStatus = PlantStates.SeedNotPlanted;
        WaterLevel = 0;
        WaterLevelMax = waterLevelMax;
        SproutWaterLevel = sproutWaterLevel;
        TreeWaterLevel = treeWaterLevel;
        PlantWaterLevel = plantWaterLevel;
    }

    public override void AddTotalPlants()
    {
        GameManager.instance.totalPlants += 1;
    }

    public override void CheckGrow()
    {
        PlantWaterLevel = WaterLevel;

        if (PlantWaterLevel >= SproutWaterLevel && plantStatus == PlantStates.SeedPlanted)
        {
            plantStatus = PlantStates.Sprout;
            CheckPlantState();
        }

        if (PlantWaterLevel >= TreeWaterLevel && plantStatus == PlantStates.Sprout)
        {
            plantStatus = PlantStates.Tree;
            CheckPlantState();
        }
    }
    #endregion

    #region L�gica para a �rvore ser plantada

    private void OnTriggerEnter(Collider other)
    {
        if (plantStatus == PlantStates.SeedNotPlanted)
        {
            if (other.TryGetComponent<Breeze>(out Breeze wind))
            {
                plantStatus = PlantStates.SeedCarried;
                gameObject.transform.position = other.transform.position;
                Wind.windEvent += IngrainPlant;
            }
        }
    }

    private void IngrainPlant()
    {    
        DestroyImmediate(gameObject.GetComponent<Carry>());
        SetPlantOnCube();
    }

    public override void CheckPlantState()
    {
        seedGameObject.SetActive(plantStatus == PlantStates.SeedNotPlanted || plantStatus == PlantStates.SeedCarried);
        seedPlantedGameObject.SetActive(plantStatus == PlantStates.SeedPlanted);
        CheckSeedState();
        sproutGameObject.SetActive(plantStatus == PlantStates.Sprout);
        treeGameObject.SetActive(plantStatus == PlantStates.Tree);
    }

    public void CheckSeedState()
    {
        if (seedGameObject.activeInHierarchy)
        {
            gameObject.TryGetComponent<Carry>(out Carry carry);
            if (plantStatus == PlantStates.SeedNotPlanted)
            {
                if (!carry)
                {
                    gameObject.AddComponent<Carry>();
                }
                
            }

            else
            {
                if (carry)
                {
                    Destroy(gameObject.GetComponent<Carry>());
                    SetPlantOnCube();
                }
            }

        }
    }

    public void SetPlantOnCube()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            if (hitInfo.collider)
            {
                GameObject blockLanded = hitInfo.collider.gameObject;
                blockLanded.TryGetComponent<Grass>(out Grass grass);

                if (grass && grass.plantable)
                    PlantSeed(grass, blockLanded);

                else
                    ReturnOriginalPos();
            }
        }

        else
            ReturnOriginalPos();
    }

    void PlantSeed(Grass grass, GameObject blockLanded)
    {
        plantStatus = PlantStates.SeedPlanted;
        CheckPlantState();
        grass.plantable = false;
        gameObject.transform.position = CalcSeedPos(blockLanded);
        Wind.windEvent -= IngrainPlant;
    }

    Vector3 CalcSeedPos(GameObject blockLanded)
    {
        return new Vector3(blockLanded.transform.position.x, blockLanded.transform.position.y + offsetPlantPositionY, blockLanded.transform.position.z);
    }

    void ReturnOriginalPos()
    {
        plantStatus = PlantStates.SeedNotPlanted;
        CheckPlantState();
        gameObject.transform.position = originalPos;

    }

    #endregion

}



