using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class Macieira : Plant
{ 
    Plant plantObject;
    public GameObject seedGameObject;
    public GameObject sproutGameObject;
    public GameObject treeGameObject;

    [InspectorName("Status da planta")]
    public PlantStates plantStatus;


    [Range(0, 50)]
    public int plantWaterLevel;
    [Range(30, 50)]
    public int waterLevelMax;
    [Range(1, 30)]
    public int sproutWaterLevel;
    [Range(2, 30)]
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
        if (WaterLevel >= SproutWaterLevel)
        {
            plantStatus = PlantStates.Sprout;
            CheckPlantState();
        }

        else if (WaterLevel >= TreeWaterLevel)
        {
            plantStatus = PlantStates.Tree;
            CheckPlantState();
        }
    }
    #endregion

    #region Lógica para a árvore ser plantada

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<WindBehavior>(out WindBehavior wind))
            WindManager.windEvent += IngrainPlant;
    }

    private void IngrainPlant()
    {
        DestroyImmediate(gameObject.GetComponent<Carry>());
        SetPlantOnCube();
    }

    public override void CheckPlantState()
    {
        seedGameObject.SetActive(plantStatus == PlantStates.SeedNotPlanted || plantStatus == PlantStates.SeedPlanted);
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
        WindManager.windEvent -= IngrainPlant;
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

    //if (plant.spawnSeeds == true && plant.plantState == Plant.plantStates.Tree)
    //{
    //    plant.spawnSeeds = false;
    //    GameObject maciera = Instantiate(macieraPrefab, transform.position, transform.rotation);
    //    maciera.transform.GetChild(0).gameObject.SetActive(true);
    //    maciera.transform.GetChild(1).gameObject.SetActive(false);
    //    maciera.transform.GetChild(2).gameObject.SetActive(false);

    //}

}



