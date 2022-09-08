using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSprout : MonoBehaviour
{
    Plant plant;
    public GameObject seeds;
    GameObject instantiateSeeds;
    public WindActive windPrefab;

    private void OnEnable()
    {
        plant = new Plant(
            plantState: Plant.plantStates.Sprout,
            iSprout: true,
            iWeed: false,
            isIngrained: false,
            canDestroy: false,
            wLevel: 0);

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wind")
        {
            //if (Wind.ActualState == Wind.windState.Released)
            //{
            //    gameObject.GetComponent<Renderer>().enabled = false;
            //    instantiateSeeds = Instantiate(seeds, gameObject.transform.position, gameObject.transform.rotation);
            //}
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Wind")
    //    {
    //        gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, 1, other.gameObject.transform.position.z);
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wind")
        {
            //GameObject go = other.gameObject;
            //if (Wind.ActualState == Wind.windState.Released)
            //{
            //    Destroy(gameObject);
            //}
        }

    }
}
