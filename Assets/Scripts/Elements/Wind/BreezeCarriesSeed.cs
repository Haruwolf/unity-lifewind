using UnityEngine;

public class BreezeCarriesSeed : MonoBehaviour
{

    [SerializeField]
    private Wind wind;

    private void OnEnable()
    {
        wind = GetComponentInParent<GetWind>().GetWindObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<PlantUpdateGrowState>(out PlantUpdateGrowState updateState);
        if (updateState == null)
            return;
        
        var actualState = updateState.GetActualPlantState();

        if (actualState == Plant.PlantStates.SeedNotPlanted || actualState == Plant.PlantStates.SeedCarried)
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
        wind.OnWindFinished.AddListener(plantSeed.IngrainPlant);       
    }

}
