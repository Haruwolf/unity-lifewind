using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Macieira : Plant
{ 
    Plant plantObject;
    public GameObject seedGameObject;
    public GameObject sproutGameObject;
    public GameObject treeGameObject;

    [Rename("Status atual da planta")]
    PlantStates plantStatus;

    [Rename("Nível de água atual da planta")]
    public int plantWaterLevel;

    [Rename("Nível máximo de água da planta")]
    public int waterLevelMax;

    [Rename("Nível requerido de água para semente se transformar em muda")]
    public int sproutWaterLevel;

    [Rename("Nível requerido de água para muda se transformar em árvore")]
    public int treeWaterLevel;

    private void OnEnable()
    {
        AttributeGameObjects();
        SetInitialPlantState();
        AddTotalPlants();       
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

    public override void CheckPlantState()
    {
        seedGameObject.SetActive(plantStatus == PlantStates.SeedNotPlanted || plantStatus == PlantStates.SeedPlanted);
        sproutGameObject.SetActive(plantStatus == PlantStates.Sprout);
        treeGameObject.SetActive(plantStatus == PlantStates.Tree);
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

    //if (plant.spawnSeeds == true && plant.plantState == Plant.plantStates.Tree)
    //{
    //    plant.spawnSeeds = false;
    //    GameObject maciera = Instantiate(macieraPrefab, transform.position, transform.rotation);
    //    maciera.transform.GetChild(0).gameObject.SetActive(true);
    //    maciera.transform.GetChild(1).gameObject.SetActive(false);
    //    maciera.transform.GetChild(2).gameObject.SetActive(false);

    //}

}



