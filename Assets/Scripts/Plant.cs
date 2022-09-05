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

    plantStates plantState = Plant.plantStates.NotSet;

    bool initialSprout;
    bool isWeed;
    bool canBeDestroyed;
    public bool isIngrained;
    int waterLevel;

    public Plant(plantStates plantState, bool iSprout, bool iWeed, bool canDestroy, int wLevel, bool isIngrained)
    {
        this.plantState = plantState;
        initialSprout = iSprout;
        canBeDestroyed = canDestroy;
        waterLevel = wLevel;
        this.isIngrained = isIngrained;
    }

    public void growStates()
    {
        if (waterLevel > 5 && plantState == plantStates.Seed)
        {
            plantState = plantStates.Sprout;
            return;
        }

        if (waterLevel > 15 && plantState == plantStates.Sprout)
        {
            plantState = plantStates.Tree;
            return;
        }

    }





}
