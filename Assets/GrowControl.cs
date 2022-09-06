using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowControl : MonoBehaviour
{
    public Plant plant;

    public GameObject seed;
    public GameObject sprout;
    public GameObject tree;
    private void OnEnable()
    {
        plant = new Plant(
            plantState: Plant.plantStates.Seed,
            iSprout: false,
            iWeed: false,
            canDestroy: false,
            wLevel: 0,
            isIngrained: false);
    }

    public void checkGrow(float wLevel)
    {
        plant.WaterLevel = wLevel;
        plant.growStates(plant.WaterLevel);

        seed.SetActive(plant.plantState == Plant.plantStates.Seed);
        sprout.SetActive(plant.plantState == Plant.plantStates.Sprout); 
        tree.SetActive(plant.plantState == Plant.plantStates.Tree);
    }

}

