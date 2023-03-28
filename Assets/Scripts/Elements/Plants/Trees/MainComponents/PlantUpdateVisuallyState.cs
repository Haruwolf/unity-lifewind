using UnityEngine;

[RequireComponent(typeof(PlantUpdateGrowState))]
public class PlantUpdateVisuallyState : MonoBehaviour
{
    [SerializeField]
    private PlantUpdateGrowState plantUpdateGrowState;

    [SerializeField]
    private PlantController plantController;

    private GameObject m_SeedGameObject, m_SeedPlantedGameObject, m_SproutGameObject, m_TreeGameObject;

    private void OnEnable()
    {
        plantUpdateGrowState.OnChangedState.AddListener(CheckPlantState);
        plantController.onPlantCreated.AddListener(SetGameObjectsList);
    }

    private void SetGameObjectsList()
    {
        var plant = plantController.GetPlantObject();
        m_SeedGameObject = plant.GetSeedGameObject();
        m_SeedPlantedGameObject = plant.GetSeedPlantedGameObject();
        m_SproutGameObject = plant.GetSproutGameObject();
        m_TreeGameObject = plant.GetTreeGameObject();
    }

    private void CheckPlantState(Plant.PlantStates newState)
    {
        m_SeedGameObject.SetActive(newState == Plant.PlantStates.SeedNotPlanted || newState == Plant.PlantStates.SeedCarried);
        m_SeedPlantedGameObject.SetActive(newState == Plant.PlantStates.SeedPlanted);
        m_SproutGameObject.SetActive(newState == Plant.PlantStates.Sprout);
        m_TreeGameObject.SetActive(newState == Plant.PlantStates.Tree);
    }
}
