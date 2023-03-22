using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlantController))]
public class PlantUpdateGrowState : MonoBehaviour
{
    [SerializeField]
    private PlantController plantController;

    private Plant plant;

    [HideInInspector]
    public UnityEvent<Plant.PlantStates> OnChangedState;

    private void OnEnable()
    {
        plantController.OnPlantCreated.AddListener(delegate { SetPlant(); });
    }

    private void SetPlant()
    {
        plant = GetPlant();
    }

    public Plant GetPlant()
    {
        return plantController.GetPlantObject();
    }

    public Plant.PlantStates GetActualPlantState()
    {
        return plant.GetPlantState();
    }

    public void CheckGrow()
    {
        Plant.PlantStates plantState = GetActualPlantState();

        if (plant.WaterLevel >= plant.SproutWaterLevel)
        {
            if (plantState == Plant.PlantStates.SeedPlanted)
            {
                plant.ChangePlantState(Plant.PlantStates.Sprout);
            }

            else if (plantState == Plant.PlantStates.Sprout && plant.WaterLevel >= plant.TreeWaterLevel)
            {
                plant.ChangePlantState(Plant.PlantStates.Tree);
            }

            Plant.PlantStates actualState = GetActualPlantState();
            OnChangedState?.Invoke(actualState);
        }
    }

}
