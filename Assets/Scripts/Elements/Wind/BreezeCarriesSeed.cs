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
        other.TryGetComponent(out Breeze windCol);
        
        if (windCol != null)
            return;
        
        other.TryGetComponent<PlantUpdateGrowState>(out PlantUpdateGrowState updateState);
        if (updateState == null)
            return;
        
        var actualState = updateState.GetActualPlantState();

        if (actualState == Plant.PlantStates.SeedNotPlanted || actualState == Plant.PlantStates.SeedCarried)
        {
            if (other.TryGetComponent<Carry>(out Carry carry))
            {
                RemoveCarry(carry);
                Plant plant = updateState.GetPlant();
                plant.ChangePlantState(Plant.PlantStates.SeedCarried);
                updateState.CheckGrow();
                IngrainPlantOnWindFinished(other.gameObject);
            }
        }
    }
    
    void RemoveCarry(Carry carry)
    {
        if (carry != null)
            Destroy(carry);
    }

    private void IngrainPlantOnWindFinished(GameObject otherGameObject)
    {
        otherGameObject.TryGetComponent<PlantIngrainSeed>(out PlantIngrainSeed plantSeed);
        wind.OnWindFinished.AddListener(plantSeed.IngrainPlant);       
    }

}
