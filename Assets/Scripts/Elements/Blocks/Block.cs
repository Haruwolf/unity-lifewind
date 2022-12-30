using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public enum blockTypes
    {
        Grass,
        Water,
    }

    blockTypes blockType;


    bool plantable;


    int _waterLevel;
    int waterLevelMax;

    public int WaterLevel
    {
        get { return _waterLevel; }
        set {
            if (value < 0)
                _waterLevel = 0;

            else if (value > waterLevelMax)
                _waterLevel = waterLevelMax;

            _waterLevel = value;

        }
    }

    public Block(bool plantable, blockTypes blockType, int wLevel, int wLevelMax)
    {
        this.plantable = plantable;
        this.blockType = blockType;
        WaterLevel = wLevel;
        this.waterLevelMax = wLevelMax;
    }




}
