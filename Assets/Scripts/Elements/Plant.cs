using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, IPlant
{
    public enum PlantStates
    {
        SeedNotPlanted,
        SeedPlanted,
        Sprout,
        Tree,
    }

    public PlantStates PlantStatus;

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

    public virtual void CheckPlantState()
    {

    }

    public virtual void CheckGrow()
    {

    }

    public virtual void AttributeGameObjects()
    {

    }

    public virtual void SetInitialPlantState()
    {

    }

    public virtual void AddTotalPlants()
    {

    }
}
