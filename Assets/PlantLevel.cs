using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLevel : MonoBehaviour
{
    Plant plant;

    public delegate void PlantEvent();
    public PlantEvent plantEvent;

    private void Start()
    {
        plant = GetComponent<Plant>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Cloud>(out var cloud);
        if (cloud != null)
        {
            WaterPlant();
        }

    }

    void WaterPlant()
    {
        plant.WaterLevel += 1;
        plantEvent();

    }


}
