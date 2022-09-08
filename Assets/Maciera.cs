using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maciera : MonoBehaviour
{
    public Plant plant;

    public GameObject seed;
    public GameObject sprout;
    public GameObject tree;

    private void Awake()
    {
        plant = new Plant(
            plantState: Plant.plantStates.Seed,
            iSprout: false,
            iWeed: false,
            canDestroy: false,
            wLevel: 0,
            isIngrained: false,
            spawnSeed: true);

    }

    public void checkGrow(float wLevel)
    {
        plant.WaterLevel = wLevel;
        plant.growStates(plant.WaterLevel);

        seed.SetActive(plant.plantState == Plant.plantStates.Seed);
        sprout.SetActive(plant.plantState == Plant.plantStates.Sprout); 
        tree.SetActive(plant.plantState == Plant.plantStates.Tree);

        if (plant.WaterLevel > 1 && plant.spawnSeeds == true && plant.plantState == Plant.plantStates.Seed)
        {
            plant.spawnSeeds = false;
            Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
        }
    }

}

