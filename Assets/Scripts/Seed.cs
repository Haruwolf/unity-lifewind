using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public Plant plant;
    public GameObject windPrefab;

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
        if (other.gameObject.tag == "Wind" && plant.isIngrained == false)
            gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, 2, other.gameObject.transform.position.z);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wind")
        {
            plant.isIngrained = true;
            gameObject.transform.SetParent(null);
            Debug.Log("teste");
        } 
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Grass" && plant.isIngrained == true)
            gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);

        if (other.gameObject.tag == "Water" && plant.isIngrained == true)
            Destroy(gameObject);

        

    }
}
