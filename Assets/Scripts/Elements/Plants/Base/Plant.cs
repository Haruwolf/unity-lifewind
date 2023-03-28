using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    private bool maxedWater = false;

    public PlantStates PlantState
    {
        get { return _plantState; }
        set { _plantState = value; }
    }

    private float _waterLevel;
    private int _waterLevelMax;
    private int _sproutLevel;
    private int _treeLevel;

    public UnityEvent onMaxedWatering { get; }
    public GameObject SeedGameObject { get; set; }
    public GameObject SeedPlantedGameObject { get; set; }
    public GameObject SproutGameObject { get; set; }
    public GameObject TreeGameObject { get; set; }
    public GameObject PlantGameObject { get; set; }
    public float WaterLevel
    {
        get => _waterLevel;
        set
        {
            if (value < _treeLevel)
                _waterLevel = value;
            
            else
            {
                _waterLevel = _waterLevelMax;
                onMaxedWatering?.Invoke();
                onMaxedWatering?.RemoveAllListeners();
                
            }
                
        }
    }

    public PlantStates GetPlantState()
    {
        return PlantState;
    }

    public int WaterLevelMax
    {
        get => _waterLevelMax;
        set => _waterLevelMax = value;
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

    public Plant(PlantStates plantState, 
        GameObject seed, 
        GameObject seedPlanted, 
        GameObject sprout, 
        GameObject tree, 
        GameObject plant, float wLevel, int wLevelMax, int wSproutLevel, int wTreeLevel)
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
        onMaxedWatering = new UnityEvent();
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
