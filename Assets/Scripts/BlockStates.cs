using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStates : MonoBehaviour
{
    public bool canSpawnCloud = true;
    public GameObject cloudGameObject;
   

    public void spawnCloud()
    {
        Vector3 cloudPos = new Vector3(transform.position.x, 2, transform.position.z);
        Instantiate(cloudGameObject, cloudPos, gameObject.transform.rotation);
    }
}
