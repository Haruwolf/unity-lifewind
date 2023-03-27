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
    

    private void OnTriggerEnter(Collider col)
    {
        col.gameObject.TryGetComponent<PlantController>(out PlantController plantContrl);
        col.gameObject.TryGetComponent<PlantUpdateGrowState>(out PlantUpdateGrowState plantGrow);
        
        if (plantContrl == null)
            return;
        
        m_PlantController = plantContrl;
        m_PlantUpdateGrowState = plantGrow;
        plant = m_PlantController.GetPlantObject();
    }

    private void OnTriggerStay(Collider col)
    {
        if (!m_PlantController)
        { 
            return;
        }

        updateGrowState = m_PlantUpdateGrowState;
        actualPlantStates = updateGrowState.GetActualPlantState();
        OnPlantWatered();

    }

    private void OnPlantWatered()
    {
        if (actualPlantStates == Plant.PlantStates.SeedNotPlanted || actualPlantStates == Plant.PlantStates.SeedCarried)
        {
            return;
        }
        
        plant.WaterLevel += wateringRate * (int)wateringSpeed;
        updateGrowState.CheckGrow();

    }
}
