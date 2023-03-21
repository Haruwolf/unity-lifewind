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

    private PlantStates PlantStatus;

    private int _waterLevel; 
    private int _waterLevelMax;
    private int _sproutLevel;
    private int _treeLevel;

    public GameObject SeedGameObject { get; set; }
    public GameObject SproutGameObject { get; set; }
    public GameObject TreeGameObject { get; set; }
    public int WaterLevel { get { return _waterLevel; } set
        {
            if (value < _waterLevelMax)
                _waterLevel = value;
            else
                _waterLevel = _waterLevelMax;
        }
    }

    public PlantStates GetPlantStates()
    {
        return PlantStatus;
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

    public Plant(PlantStates plantStates, GameObject seed, GameObject sprout, GameObject tree, int wLevel, int wLevelMax, int wSproutLevel, int wTreeLevel)
    {
        PlantStatus = plantStates;
        SeedGameObject = seed;
        SproutGameObject = sprout;
        TreeGameObject = tree;
        WaterLevel = wLevel;
        WaterLevelMax = wLevelMax;
        SproutWaterLevel = wSproutLevel;
        TreeWaterLevel = wTreeLevel;
    }
}
