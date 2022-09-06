using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weed : MonoBehaviour
{
    public Plant plant;

    public GameObject weed;

    private void OnEnable()
    {
        plant = new Plant(
            plantState: Plant.plantStates.NotSet,
            iSprout: false,
            iWeed: true,
            canDestroy: true,
            wLevel: 0,
            isIngrained: true);
    }

   

}

