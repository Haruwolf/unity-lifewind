using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLevel : MonoBehaviour
{
    //Quem tem que fazer isso é a nuvem.
    Plant plant;
    Cloud cloudWatering;
    public delegate void PlantEvent();
    public PlantEvent plantEvent;

    private void Start()
    {
        plant = GetComponent<Plant>();
        Debug.Log("Teste");
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Cloud>(out var cloud);
        if (cloud != null)
        {
            cloudWatering = cloud;
            WaterPlant();
            
        }

    }

    void WaterPlant()
    {
        plant.WaterLevel += 1;
        plantEvent();
        if (cloudWatering != null)
            Invoke(nameof(WaterPlant), 0.5f);

    }
}
