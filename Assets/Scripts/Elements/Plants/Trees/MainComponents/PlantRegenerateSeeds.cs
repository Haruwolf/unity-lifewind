using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class PlantRegenerateSeeds : MonoBehaviour
{
    [SerializeField]
    PlantController plantController;
    
    [SerializeField]
    GameObject plantToRegenerate;

    private Plant m_Plant;

    private UnityEvent m_OnMaxedWatering;

    private Collider m_ActualBlock;

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
        List<Collider> plantableSpaces = RecoverGrassPlantables();
        
        if (plantableSpaces.Count == 0)
            return;

        int randomSpace = UnityEngine.Random.Range(0, plantableSpaces.Count - 1);
        
        GameObject plantPrefab = Resources.Load<GameObject>("Plants/" + gameObject.name);
        
        if (plantPrefab == null)
        {
            Debug.LogError("Failed to load plant prefab for " + gameObject.name);
            return;
        }
        
        GameObject newSeed =
            Instantiate(plantPrefab, 
                plantableSpaces[randomSpace].gameObject.transform.position, 
                transform.rotation);

        ChangeGameObjectName(newSeed);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_ActualBlock == null)
            return;
        
        //Substituir esse 0.25f por alguma coisa controlável no Inspector
        Gizmos.DrawWireCube(m_ActualBlock.gameObject.transform.position, Vector3.one);
        
    }
    
    

    private List<Collider> RecoverGrassPlantables()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 0.25F);
        Collider plantedBlock = hitColliders[0];

        foreach (Collider col in hitColliders)
        {
            Debug.Log(col.gameObject.name);
            col.gameObject.TryGetComponent<Grass>(out Grass grass);
            if (grass)
            {
                plantedBlock = col;
            }
        }

        //Ver que posição ele tá pegando esse bloco;
        //Ver se é o bloco certo também
        m_ActualBlock = plantedBlock;
        
        Collider[] adjacentBlocks = Physics.OverlapBox(plantedBlock.transform.position, Vector3.one);
        List<Collider> plantableBlocks = new List<Collider>();

        foreach (var collider in adjacentBlocks)
        {
            collider.gameObject.TryGetComponent<Grass>(out var grass);
            
            if (!grass)
                continue;
            
            if (grass.plantable)
                plantableBlocks.Add(collider);
        }

        return plantableBlocks;

    }

    private void DrawDebugBox(Collider hitCollider)
    {
        Vector3 boxCenter = hitCollider.transform.position;
        Vector3 boxSize = new Vector3(0.25f, 0.25f, 0.25f);

        Debug.Log("Desenhando caixa em: " + boxCenter.ToString() + ", tamanho: " + boxSize.ToString());

        // Desenha uma caixa em modo de arame na cena
        Vector3 halfSize = boxSize / 2.0f;
        Vector3 topLeftFront = boxCenter + new Vector3(-halfSize.x, halfSize.y, -halfSize.z);
        Vector3 topRightFront = boxCenter + new Vector3(halfSize.x, halfSize.y, -halfSize.z);
        Vector3 bottomLeftFront = boxCenter + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);
        Vector3 bottomRightFront = boxCenter + new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
        Vector3 topLeftBack = boxCenter + new Vector3(-halfSize.x, halfSize.y, halfSize.z);
        Vector3 topRightBack = boxCenter + new Vector3(halfSize.x, halfSize.y, halfSize.z);
        Vector3 bottomLeftBack = boxCenter + new Vector3(-halfSize.x, -halfSize.y, halfSize.z);
        Vector3 bottomRightBack = boxCenter + new Vector3(halfSize.x, -halfSize.y, halfSize.z);

        Color color = Color.red;
        float duration = 0.0f; // Mantém a caixa na cena por um frame
        Debug.DrawLine(topLeftFront, topRightFront, color, duration);
        Debug.DrawLine(topRightFront, bottomRightFront, color, duration);
        Debug.DrawLine(bottomRightFront, bottomLeftFront, color, duration);
        Debug.DrawLine(bottomLeftFront, topLeftFront, color, duration);

        Debug.DrawLine(topLeftBack, topRightBack, color, duration);
        Debug.DrawLine(topRightBack, bottomRightBack, color, duration);
        Debug.DrawLine(bottomRightBack, bottomLeftBack, color, duration);
        Debug.DrawLine(bottomLeftBack, topLeftBack, color, duration);

        Debug.DrawLine(topLeftFront, topLeftBack, color, duration);
        Debug.DrawLine(topRightFront, topRightBack, color, duration);
        Debug.DrawLine(bottomRightFront, bottomRightBack, color, duration);
        Debug.DrawLine(bottomLeftFront, bottomLeftBack, color, duration);
        
    }

    private void ChangeGameObjectName(GameObject regenSeed)
    {
        regenSeed.name = plantToRegenerate.name;
    }
    
}
