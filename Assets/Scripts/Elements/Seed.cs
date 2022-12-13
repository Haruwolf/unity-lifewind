using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public GameObject rootGameObject;
    public Plant rootPlantGameObject;
    Vector3 originalPos;
    ParticleSystem.MainModule particle;

    private void OnEnable()
    {
        originalPos = rootGameObject.transform.position;
        AudioControl.instance.audioSource.clip = (AudioClip)Resources.Load("PlantPop");
        AudioControl.instance.audioSource.Play();
        AudioControl.instance.audioSource.loop = false;
        particle = gameObject.GetComponent<ParticleSystem>().main;
        particle.startColor = Color.yellow;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<WindBehavior>(out WindBehavior wind))
            WindManager.windEvent += IngrainPlant;
    }

    private void IngrainPlant()
    {
        SetPlantOnCube();
        Destroy(gameObject.GetComponent<Carry>());
    }

    private void OnTriggerStay(Collider other)
    {

        //if (other.gameObject.tag == "Cloud")
        //{
        //    if (plantGameObject.plant.isIngrained == true)
        //    {

        //        plantGameObject.plant.WaterLevel += 0.5f * Time.deltaTime;
        //        plantGameObject.plant.growStates(plantGameObject.plant.WaterLevel);
        //        plantGameObject.checkGrow(plantGameObject.plant.WaterLevel);
        //    }

        //}

    }

    public void SetPlantOnCube()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            if (!hitInfo.collider)
            {
                GameObject blockLanded = hitInfo.collider.gameObject;
                blockLanded.TryGetComponent<Grass>(out Grass grass);

                if (!grass && grass.plantable)
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
        particle.startColor = Color.blue;
        grass.plantable = false;
        //plantGameObject.blockLanded = blockLanded;
        gameObject.transform.position = CalcSeedPos(blockLanded);
        WindManager.windEvent -= IngrainPlant;
    }

    Vector3 CalcSeedPos(GameObject blockLanded)
    {
        return new Vector3(blockLanded.transform.position.x, blockLanded.transform.position.y + 2, blockLanded.transform.position.z);
    }

    void ReturnOriginalPos()
    {
        rootPlantGameObject.transform.position = originalPos;
        //plantGameObject.plant.isIngrained = false;
    }
}
