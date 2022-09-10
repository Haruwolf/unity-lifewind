using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree: MonoBehaviour
{
    public Plant plant;
    public WindActive windPrefab;
    Maciera plantGameObject;

    private void OnEnable()
    {
        plantGameObject = gameObject.transform.parent.GetComponent<Maciera>();
        GameManager.instance.treesOnScreen += 1;
        AudioControl.instance.audioSource.clip = (AudioClip)Resources.Load("PlantPop");
        AudioControl.instance.audioSource.Play();
        AudioControl.instance.audioSource.loop = false;

    }

    private void OnDisable()
    {
        GameManager.instance.treesOnScreen -= 1;
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
                plantGameObject.plant.DryLevel += 2 * Time.deltaTime;
            }

        }
    }



    private void OnTriggerEnter(Collider other)
    {
        //if (plantGameObject.plant.isIngrained == true)
        //{
        //    switch (other.gameObject.tag)
        //    {
        //        case "Grass":
        //            gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
        //            break;
        //        case "Water":
        //            Destroy(gameObject);
        //            break;
        //        case "OoB":
        //            Destroy(gameObject);
        //            break;

        //        default:  
        //            break;
        //    }
        //}
    }
}
