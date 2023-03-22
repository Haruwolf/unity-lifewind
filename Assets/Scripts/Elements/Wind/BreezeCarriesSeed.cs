using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BreezeCarriesSeed : MonoBehaviour
{

    // private void OnTriggerEnter(Collider other)
    // {
    //     other.TryGetComponent<PlantUpdateGrowState>(out PlantUpdateGrowState updateState);
    //     Plant.PlantStates actualState = updateState.GetActualPlantState();


    //     if (actualState == Plant.PlantStates.SeedNotPlanted)
    //     {
    //         if (other.TryGetComponent<Carry>(out Carry carry))
    //         {
    //             Plant plant = updateState.GetPlant();
    //             plant.ChangePlantState(Plant.PlantStates.SeedCarried);
    //             updateState.CheckGrow();
    //             other.TryGetComponent<PlantOriginalPos>(out PlantOriginalPos savePos);
    //             savePos.OriginalPos = gameObject.transform.position;
    //             gameObject.transform.position = other.transform.position;
    //             other.TryGetComponent<PlantIngrainSeed>(out PlantIngrainSeed plantSeed);
    //             Wind.OnWindFinished += plantSeed.IngrainPlant;
    //         }
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<PlantUpdateGrowState>(out PlantUpdateGrowState updateState);
        Plant.PlantStates actualState = updateState.GetActualPlantState();

        if (actualState == Plant.PlantStates.SeedNotPlanted)
        {
            if (other.TryGetComponent<Carry>(out Carry carry))
            {
                Plant plant = updateState.GetPlant();
                plant.ChangePlantState(Plant.PlantStates.SeedCarried);
                updateState.CheckGrow();
                SetObjectPosition(other.gameObject);
                IngrainPlantOnWindFinished(other.gameObject);
            }
        }
    }

    private void SetObjectPosition(GameObject otherGameObject)
    {
        otherGameObject.TryGetComponent<PlantOriginalPos>(out PlantOriginalPos savePos);
        savePos.OriginalPos = gameObject.transform.position;
        gameObject.transform.position = otherGameObject.transform.position;
    }

    private void IngrainPlantOnWindFinished(GameObject otherGameObject)
    {
        otherGameObject.TryGetComponent<PlantIngrainSeed>(out PlantIngrainSeed plantSeed);
        Wind.OnWindFinished += plantSeed.IngrainPlant;
    }

}
