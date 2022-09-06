using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public Plant plant;
    public WindActive windPrefab;

    private void OnEnable()
    {
        plant = new Plant(
            plantState: Plant.plantStates.Seed,
            iSprout: false,
            iWeed: false,
            canDestroy: false,
            wLevel: 0,
            isIngrained: false);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Wind")
        { 
            if (plant.isIngrained == false && Wind.ActualState == Wind.windState.Released)
            {
                gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, 2, other.gameObject.transform.position.z);
            }
        }           
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wind" && Wind.ActualState == Wind.windState.Released)
        {
            plant.isIngrained = true;
            gameObject.transform.SetParent(null);
        } 
    }


    private void OnTriggerEnter(Collider other)
    {
        if (plant.isIngrained == true)
        {
            switch (other.gameObject.tag)
            {
                case "Grass":
                    gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
                    break;
                case "Water":
                    Destroy(gameObject);
                    break;
                case "OoB":
                    Destroy(gameObject);
                    break;

                default:  
                    break;
            }
        }
    }
}
