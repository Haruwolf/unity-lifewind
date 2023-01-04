using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class AppleTree : Plant
{ 
    Plant plantObject;
    public GameObject seedGameObject;
    public GameObject sproutGameObject;
    public GameObject treeGameObject;

    [InspectorName("Status da planta")]
    public PlantStates plantStatus;


    [Range(0, 100)]
    public int plantWaterLevel;
    [Range(30, 100)]
    public int waterLevelMax;
    [Range(1, 98)]
    public int sproutWaterLevel;
    [Range(2, 99)]
    public int treeWaterLevel;
    Vector3 originalPos;

    [InspectorName("Semente Solta")]
    public Color notPlantedColor;
    [InspectorName("Semente Plantada")]
    public Color plantedColor;

    [Range(0,3)]
    public float offsetPlantPositionY;

    #region Inicialização de objeto
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

    #region Lógica para a árvore ser plantada

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


        //Colocar condição que a semente só retorna se não estiver plantada ou carregada pelo vento.
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



