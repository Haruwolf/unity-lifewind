using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLevel : MonoBehaviour
{
    //Quem tem que fazer isso é a nuvem.
    PlantController plant;

    Plant plantComponent;
    Cloud cloudWatering;
    public delegate void PlantEvent();
    public PlantEvent plantEvent;

    private void Start()
    {
        plant = GetComponent<PlantController>();
        // plantComponent = plant.GetPlantObject();
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
        // plantComponent.PlantWaterLevel += 1;
        plantEvent?.Invoke();
        if (cloudWatering != null)
            Invoke(nameof(WaterPlant), 0.5f);

    }
}
