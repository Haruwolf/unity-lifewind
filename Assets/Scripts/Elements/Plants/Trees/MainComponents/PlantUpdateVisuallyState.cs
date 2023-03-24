using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlantUpdateGrowState))]
public class PlantUpdateVisuallyState : MonoBehaviour
{
    [SerializeField]
    private PlantUpdateGrowState plantUpdateGrowState;

    [SerializeField]
    private PlantController plantController;

    private GameObject seedGameObject, seedPlantedGameObject, sproutGameObject, treeGameObject, plantGameObject;

    private void OnEnable()
    {
        plantUpdateGrowState.OnChangedState.AddListener(CheckPlantState);
        plantController.OnPlantCreated.AddListener(SetGameObjectsList);
    }

    private void SetGameObjectsList()
    {
        Plant plant = plantController.GetPlantObject();
        plantGameObject = plant.GetPlantGameObject();
        seedGameObject = plant.GetSeedGameObject();
        seedPlantedGameObject = plant.GetSeedPlantedGameObject();
        sproutGameObject = plant.GetSproutGameObject();
        treeGameObject = plant.GetTreeGameObject();
    }

    public void CheckPlantState(Plant.PlantStates newState)
    {
        seedGameObject.SetActive(newState == Plant.PlantStates.SeedNotPlanted || newState == Plant.PlantStates.SeedCarried);
        seedPlantedGameObject.SetActive(newState == Plant.PlantStates.SeedPlanted);
        sproutGameObject.SetActive(newState == Plant.PlantStates.Sprout);
        treeGameObject.SetActive(newState == Plant.PlantStates.Tree);
    }
}
