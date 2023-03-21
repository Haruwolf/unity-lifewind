using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantRegenerateSeeds : MonoBehaviour
{
    [SerializeField]
    PlantController plantController;
    [SerializeField]
    GameObject plantToRegenerate;
    // Start is called before the first frame update
    private void Awake()
    {
        plantController.OnMaxedLevel.AddListener(RegenerateSeeds);
        
    }

    private void RegenerateSeeds()
    {
        
        GameObject regenSeed = Instantiate(plantToRegenerate, plantToRegenerate.transform.position, plantToRegenerate.transform.rotation);
        plantController.OnMaxedLevel.RemoveAllListeners();
        // ChangeGameObjectName(regenSeed);
        Debug.Log("teste");
    }

    private void ChangeGameObjectName(GameObject regenSeed)
    {
        regenSeed.name = plantToRegenerate.name;
    }

}
