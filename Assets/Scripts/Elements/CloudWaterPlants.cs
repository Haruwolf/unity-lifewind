using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloudWaterPlants : MonoBehaviour
{

    private Plant plant;
    private PlantUpdateGrowState updateGrowState;
    private Plant.PlantStates actualPlantStates;
    private PlantController m_PlantController;
    private PlantUpdateGrowState m_PlantUpdateGrowState;

    [SerializeField]
    private int wateringRate = 1;

    [SerializeField]
    private float wateringSpeed = 5;
    
    private List<PlantController> plantControllers = new List<PlantController>();

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<PlantController>(out PlantController plantContrl))
        {
            plantControllers.Add(plantContrl);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlantController>(out PlantController plantContrl))
        {
            plantControllers.Remove(plantContrl);
        }
    }
    
    private void OnPlantWatered(PlantController plantContrl)
    {
        var plantObject = plantContrl.GetPlantObject();
        var updateGrowState = plantContrl.gameObject.GetComponent<PlantUpdateGrowState>();
        var actualPlantStates = updateGrowState.GetActualPlantState();

        if (actualPlantStates == Plant.PlantStates.SeedNotPlanted || actualPlantStates == Plant.PlantStates.SeedCarried)
        {
            return;
        }
        
        plantObject.WaterLevel += wateringRate * wateringSpeed * Time.deltaTime;
        updateGrowState.CheckGrow();
    }

    private void OnTriggerStay(Collider col)
    {
        foreach (PlantController plantContrl in plantControllers)
        {
            OnPlantWatered(plantContrl);
        }
    }

}
