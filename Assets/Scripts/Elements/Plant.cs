using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant
{
    public enum plantStates
    {
        NotSet,
        Seed,
        Sprout,
        Tree,
        Weed,
    }

    public plantStates plantState = Plant.plantStates.NotSet;

    public bool initialSprout;
    public bool isWeed;
    public bool canBeDestroyed;
    public bool isIngrained;
    public bool spawnSeeds;
    private float _waterLevel;

    public float WaterLevel { get { return _waterLevel; } set
        {
            if (value < 20f)
                _waterLevel = value;
            else
                _waterLevel = 20f;
        }
    }

    public Plant(plantStates plantState, bool iSprout, bool iWeed, bool canDestroy, float wLevel, bool isIngrained, bool spawnSeed)
    {
        this.plantState = plantState;
        initialSprout = iSprout;
        canBeDestroyed = canDestroy;
        WaterLevel = wLevel;
        this.isIngrained = isIngrained;
        this.spawnSeeds = spawnSeed;
    }

    public virtual void growStates(float wLevel)
    {
        if (wLevel > 5 && plantState == plantStates.Seed)
        {
            plantState = plantStates.Sprout;
            return;
        }

        if (wLevel > 15 && plantState == plantStates.Sprout)
        {
            plantState = plantStates.Tree;
            return;
        }

    }







}
