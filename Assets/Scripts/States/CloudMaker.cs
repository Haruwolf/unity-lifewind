using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CloudMaker : MonoBehaviour
{
    public bool canSpawnCloud = true;
    public GameObject cloudGameObject;
    public float riverHP = 100;
    public bool riverCooldown = false;

    public float cdTimer = 3;



    //public void spawnCloud()
    //{
    //    Vector3 cloudPos = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
    //    Instantiate(cloudGameObject, cloudPos, cloudGameObject.transform.rotation);
    //}

    private void Update()
    {
        if (riverHP < 0)
            riverCooldown = true;

        if (riverCooldown == true)
        {
            cdTimer -= 1 * Time.deltaTime;
            if (cdTimer <= 0)
            {
                cdTimer = 3;
                riverCooldown = false;
            }
        }

        if (riverHP < 100)
            riverHP += 0.1f * Time.deltaTime;

        Debug.Log(riverHP);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cloud")
            canSpawnCloud = false;

        //Debug.Log(canSpawnCloud);
    }

    private void OnTriggerExit(Collider other)
    {    
        canSpawnCloud = true;
        //Debug.Log(canSpawnCloud);
    }
}
