using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlantOriginalPos))]
[RequireComponent(typeof(PlantUpdateGrowState))]
public class PlantIngrainSeed : MonoBehaviour
{
    [SerializeField]
    private PlantUpdateGrowState updateGrowState;

    public void IngrainPlant()
    {
        DestroyImmediate(gameObject.GetComponent<Carry>());
        SetPlantOnCube();
    }

    public void SetPlantOnCube()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 0.25F);
        foreach (Collider col in hitColliders)
        {

            col.gameObject.TryGetComponent<Grass>(out Grass grass);
            if (grass != null)
            {
                if (grass.plantable)
                {
                    PlantSeed(grass, col.gameObject);
                    return;
                }

                else
                    ReturnOriginalPos();
            }

        }


    }

    bool m_Started = true;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, 0.25F);
    }

    void PlantSeed(Grass grass, GameObject blockLanded)
    {
        Plant plant = updateGrowState.GetPlant();
        plant.ChangePlantState(Plant.PlantStates.SeedPlanted);
        updateGrowState.CheckGrow();
        gameObject.transform.position = blockLanded.transform.position;
        SetGrassPlantable(grass, false);
    }

    void ReturnOriginalPos()
    {
        Plant plant = updateGrowState.GetPlant();
        Debug.Log(plant);
        plant.ChangePlantState(Plant.PlantStates.SeedNotPlanted);
        updateGrowState.CheckGrow();
        ResetPlantPosition();
    }

    private void SetGrassPlantable(Grass grass, bool plantable)
    {
        grass.plantable = plantable;
    }

    private void ResetPlantPosition()
    {
        GameObject plantGameObject = updateGrowState.GetPlant().GetPlantGameObject();
        plantGameObject.TryGetComponent<PlantOriginalPos>(out PlantOriginalPos posSaved);
        plantGameObject.transform.position = posSaved.GetOriginalPos();
    }

}
