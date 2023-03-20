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
        //plantController.OnMaxedLevel.AddListener(delegate {RegenerateSeeds();});
        //Debug.Log("teste");
    }

    private void RegenerateSeeds()
    {
        GameObject regenSeed = Instantiate(plantToRegenerate, plantToRegenerate.transform.position, plantToRegenerate.transform.rotation);
        ChangeGameObjectName(regenSeed);
    }

    private void ChangeGameObjectName(GameObject regenSeed)
    {
        regenSeed.name = plantToRegenerate.name;
    }

}
