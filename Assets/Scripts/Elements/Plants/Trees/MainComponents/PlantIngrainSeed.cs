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

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            if (hitInfo.collider)
            {
                GameObject blockLanded = hitInfo.collider.gameObject;
                blockLanded.TryGetComponent<Grass>(out Grass grass);

                if (grass && grass.plantable)
                    PlantSeed(grass, blockLanded);

                else
                    ReturnOriginalPos();
            }
        }

        else
            ReturnOriginalPos();
    }

    void PlantSeed(Grass grass, GameObject blockLanded)
    {
        Plant plant = updateGrowState.GetPlant();
        plant.ChangePlantState(Plant.PlantStates.SeedPlanted);
        updateGrowState.CheckGrow();
        SetGrassPlantable(grass, false);
        RemoveIngrainPlantOnWindFinished();
    }

    void ReturnOriginalPos()
    {
        Plant plant = updateGrowState.GetPlant();
        plant.ChangePlantState(Plant.PlantStates.SeedNotPlanted);
        updateGrowState.CheckGrow();
        ResetPlantPosition();
    }

    private void SetGrassPlantable(Grass grass, bool plantable)
    {
        grass.plantable = plantable;
    }

    private void RemoveIngrainPlantOnWindFinished()
    {
        Wind.OnWindFinished -= IngrainPlant;
    }

    private void ResetPlantPosition()
    {
        GameObject plantGameObject = updateGrowState.GetPlant().GetPlantGameObject();
        plantGameObject.TryGetComponent<PlantOriginalPos>(out PlantOriginalPos posSaved);
        plantGameObject.transform.position = posSaved.GetOriginalPos();
    }

}
