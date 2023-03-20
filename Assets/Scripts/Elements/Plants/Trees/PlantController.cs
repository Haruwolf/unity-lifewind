using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;


[RequireComponent(typeof(Plant))]
[RequireComponent(typeof(PlantLevel))]
[RequireComponent(typeof(PlantCollisionConfig))]
[AddComponentMenu(nameof(Carry))]
public class PlantController : Plant
{ 
    private Plant plantObject;

    [SerializeField]
    [Tooltip("Adicione aqui os prefabs de semente, muda e árvore respectivamente.")]
    private GameObject seedGameObject, sproutGameObject, treeGameObject;

    [SerializeField]
    [Header("Status da planta")]
    [Tooltip("Altere aqui qual o status atual da planta durante o jogo para semente, muda ou árvore.")]
    private PlantStates plantStatus;

    [SerializeField]
    [Range(0, 100)]
    [Tooltip("Nível atual da água da planta, não precisa ser alterado, está no inspector para própositos de assistir a velocidade em que o nível de água sobe durante o jogo.")]
    private int plantWaterLevel;

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

    [SerializeField]
    [Header("Cores dos status da semente")]
    [Tooltip("Cor da semente quando não está plantada.")]
    [ColorUsage(true)]
    private Color notPlantedColor = Color.white;

    [SerializeField]
    [ColorUsage(true)]
    [Tooltip("Cor da semente quando está plantada.")]
    private Color plantedColor;

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
    }

    public override void AddTotalPlants()
    {
        GameManager.instance.totalPlants += 1;
    }

    public override void CheckGrow()
    {
        plantWaterLevel = WaterLevel;

        if (plantWaterLevel >= SproutWaterLevel && plantStatus == PlantStates.SeedPlanted)
        {
            plantStatus = PlantStates.Sprout;
            CheckPlantState();
        }

        if (plantWaterLevel >= TreeWaterLevel && plantStatus == PlantStates.Sprout)
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
        seedGameObject.SetActive(plantStatus == PlantStates.SeedNotPlanted || plantStatus == PlantStates.SeedPlanted || plantStatus == PlantStates.SeedCarried);
        CheckSeedState();
        sproutGameObject.SetActive(plantStatus == PlantStates.Sprout);
        treeGameObject.SetActive(plantStatus == PlantStates.Tree);
    }

    public void CheckSeedState()
    {
        if (seedGameObject.activeInHierarchy)
        {
            seedGameObject.TryGetComponent<ParticleSystem>(out ParticleSystem particle);
            gameObject.TryGetComponent<Carry>(out Carry carry);
            if (plantStatus == PlantStates.SeedNotPlanted)
            {
                if (!carry)
                {
                    gameObject.AddComponent<Carry>();
                }
                SetParticleColor(particle, notPlantedColor);
                
            }

            else
            {
                if (carry)
                {
                    Destroy(gameObject.GetComponent<Carry>());
                    SetPlantOnCube();
                }
                SetParticleColor(particle, plantedColor);
            }

        }
    }

    void SetParticleColor(ParticleSystem particle, Color particleColor)
    {
        var main = particle.main;
        main.startColor = particleColor;
    }

    public void SetPlantOnCube()
    {
        Ray ray = new Ray(transform.position, Vector3.down);


        //Colocar condi��o que a semente s� retorna se n�o estiver plantada ou carregada pelo vento.
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

    //Balanceamento de mecanica
    //if (plant.spawnSeeds == true && plant.plantState == Plant.plantStates.Tree)
    //{
    //    plant.spawnSeeds = false;
    //    GameObject maciera = Instantiate(macieraPrefab, transform.position, transform.rotation);
    //    maciera.transform.GetChild(0).gameObject.SetActive(true);
    //    maciera.transform.GetChild(1).gameObject.SetActive(false);
    //    maciera.transform.GetChild(2).gameObject.SetActive(false);

    //}

}



