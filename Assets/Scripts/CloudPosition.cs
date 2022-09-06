using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPosition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
    }
    private void OnTriggerStay(Collider other)
    { 
        if (other.gameObject.tag == "Wind" && Wind.ActualState == Wind.windState.Released)
        {
            gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, 2, other.gameObject.transform.position.z);
            Debug.Log("entered");
        }
    }
}
