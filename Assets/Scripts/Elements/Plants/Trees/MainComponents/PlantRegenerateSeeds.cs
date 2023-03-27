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
        GameObject plantPrefab = Resources.Load<GameObject>("Plants/" + gameObject.name);
        if (plantPrefab == null)
        {
            Debug.LogError("Failed to load plant prefab for " + gameObject.name);
            return;
        }
        
        GameObject newSeed =
            Instantiate(plantPrefab, 
                gameObject.transform.position, 
                transform.rotation);

        ChangeGameObjectName(newSeed);
    }

    private void ChangeGameObjectName(GameObject regenSeed)
    {
        regenSeed.name = plantToRegenerate.name;
    }
    
}
