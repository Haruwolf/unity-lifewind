using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprout: MonoBehaviour
{
    public Plant plant;
    public WindActive windPrefab;
    BasePlant plantGameObject;
    public GameObject plantPop;

    private void OnEnable()
    {
        
        plantGameObject = gameObject.transform.parent.GetComponent<BasePlant>();
        Instantiate(plantPop, gameObject.transform.position, gameObject.transform.rotation);
        GameManager.instance.sproutsOnScreen += 1;
        AudioControl.instance.audioSource.clip = (AudioClip)Resources.Load("PlantPop");
        AudioControl.instance.audioSource.Play();
        AudioControl.instance.audioSource.loop = false;

    }

    private void OnDisable()
    {
        //GameManager.instance.sproutsOnScreen -= 1;
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.tag == "Wind")
        //{
            
        //}

        if(other.gameObject.tag == "Cloud")
        {
            if (plantGameObject.plant.isIngrained == true)
            {
                plantGameObject.plant.WaterLevel += 0.5f * Time.deltaTime;
                plantGameObject.plant.growStates(plantGameObject.plant.WaterLevel);
                plantGameObject.checkGrow(plantGameObject.plant.WaterLevel);
                plantGameObject.plant.DryLevel += 8 * Time.deltaTime;

                
            }

        }
    }

}
