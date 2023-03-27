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

    //Adicionar uma propriedade para alterar o tamanho 
    private void SetPlantOnCube()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 0.25F);
        foreach (var col in hitColliders)
        {

            col.gameObject.TryGetComponent(out Grass grass);
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

            else
            {
                ReturnOriginalPos();
            }

        }


    }
    
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
        RemoveCarry();
        gameObject.transform.position = blockLanded.transform.position;
        SetGrassPlantable(grass, false);
    }

    void RemoveCarry()
    {
        gameObject.TryGetComponent<Carry>(out var carry);
        if (carry != null)
            Destroy(carry);
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

    private void CheckCarry()
    {
        gameObject.TryGetComponent<Carry>(out var carry);

        if (carry == null)
            gameObject.AddComponent<Carry>();
    }

    private void ResetPlantPosition()
    {
        CheckCarry();
        gameObject.TryGetComponent(out PlantOriginalPos posSaved);
        gameObject.transform.position = posSaved.GetOriginalPos();
        gameObject.GetComponentInChildren<ParticleSystem>()?.Play();
    }

}
