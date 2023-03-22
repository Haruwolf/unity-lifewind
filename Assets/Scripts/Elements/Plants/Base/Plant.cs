using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : IPlant
{
    public enum PlantStates
    {
        SeedNotPlanted,
        SeedCarried,
        SeedPlanted,
        Sprout,
        Tree,
    }

    private PlantStates _plantState;

    public PlantStates PlantState
    {
        get { return _plantState; }
        set { _plantState = value; }
    }

    private int _waterLevel;
    private int _waterLevelMax;
    private int _sproutLevel;
    private int _treeLevel;

    public GameObject SeedGameObject { get; set; }
    public GameObject SeedPlantedGameObject { get; set; }
    public GameObject SproutGameObject { get; set; }
    public GameObject TreeGameObject { get; set; }
    public GameObject PlantGameObject { get; set; }
    public int WaterLevel
    {
        get { return _waterLevel; }
        set
        {
            if (value < _waterLevelMax)
                _waterLevel = value;
            else
                _waterLevel = _waterLevelMax;
        }
    }

    public PlantStates GetPlantState()
    {
        return PlantState;
    }

    public int WaterLevelMax
    {
        get { return _waterLevelMax; }
        set { _waterLevelMax = value; }
    }

    public int SproutWaterLevel
    {
        get { return _sproutLevel; }
        set
        {
            if (value < 1)
                _sproutLevel = 1;

            _sproutLevel = value;
        }
    }

    public int TreeWaterLevel
    {
        get { return _treeLevel; }
        set
        {
            if (value < SproutWaterLevel)
                _treeLevel = SproutWaterLevel + 1;

            _treeLevel = value;
        }
    }

    public Plant(PlantStates plantState, GameObject seed, GameObject seedPlanted, GameObject sprout, GameObject tree, GameObject plant, int wLevel, int wLevelMax, int wSproutLevel, int wTreeLevel)
    {
        PlantState = plantState;
        SeedGameObject = seed;
        SeedPlantedGameObject = seedPlanted;
        SproutGameObject = sprout;
        TreeGameObject = tree;
        PlantGameObject = plant;
        WaterLevel = wLevel;
        WaterLevelMax = wLevelMax;
        SproutWaterLevel = wSproutLevel;
        TreeWaterLevel = wTreeLevel;
    }

    public GameObject GetSeedGameObject()
    {
        return SeedGameObject;
    }

    public GameObject GetPlantGameObject()
    {
        return PlantGameObject;
    }

    public GameObject GetSeedPlantedGameObject()
    {
        return SeedPlantedGameObject;
    }

    public GameObject GetSproutGameObject()
    {
        return SproutGameObject;
    }

    public GameObject GetTreeGameObject()
    {
        return TreeGameObject;
    }

    public PlantStates ChangePlantState(PlantStates plantState)
    {
        PlantState = plantState;
        return PlantState;
    }
}
