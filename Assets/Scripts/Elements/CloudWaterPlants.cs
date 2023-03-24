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

    [SerializeField]
    private int wateringRate = 1;

    [SerializeField]
    private float wateringSpeed = 0.25f;


    private void OnTriggerEnter(Collider col)
    {

        //Separar isso aqui em duas funções
        col.gameObject.TryGetComponent<PlantController>(out PlantController plantContrl);
        col.gameObject.TryGetComponent<PlantUpdateGrowState>(out PlantUpdateGrowState plantGrow);

        if (plantContrl)
        {
            updateGrowState = plantGrow;
            plant = plantContrl.GetPlantObject();
            actualPlantStates = updateGrowState.GetActualPlantState();
            OnPlantWatered();
        }



    }

    private void OnPlantWatered()
    {
        if (actualPlantStates != Plant.PlantStates.SeedNotPlanted || actualPlantStates != Plant.PlantStates.SeedCarried)
        {
            plant.WaterLevel += wateringRate;
            updateGrowState.CheckGrow();
            Invoke(nameof(OnPlantWatered), wateringSpeed);
        }
    }
}
