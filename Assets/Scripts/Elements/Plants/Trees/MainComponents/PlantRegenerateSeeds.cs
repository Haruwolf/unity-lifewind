using System;
using UnityEngine;
using UnityEngine.Events;

public class PlantRegenerateSeeds : MonoBehaviour
{
    [SerializeField]
    PlantController plantController;
    
    [SerializeField]
    GameObject plantToRegenerate;

    private Plant m_Plant;

    private UnityEvent m_OnMaxedWatering;

    private void OnEnable()
    {
        plantController.onPlantCreated.AddListener(AddListenerOnMaxedWater);
    }

    private void AddListenerOnMaxedWater()
    {
        m_Plant = plantController.GetPlantObject();
        m_Plant?.onMaxedWatering.AddListener(delegate { RegenerateSeeds(m_Plant); });

    }

    private void RegenerateSeeds(Plant plant)
    {
        var actualState = plant.GetPlantState();
        Debug.Log(actualState);
        Debug.Log(gameObject.name + plant.WaterLevel);
        
        if (actualState != Plant.PlantStates.Tree)
            return;
        
        GameObject newSeed =
            Instantiate(Resources.Load<GameObject>("Plants/"+gameObject.name), 
                gameObject.transform.position, 
                transform.rotation);

        //ChangeGameObjectName(newSeed);
    }

    private void ChangeGameObjectName(GameObject regenSeed)
    {
        regenSeed.name = plantToRegenerate.name;
    }
    
}
