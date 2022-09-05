using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPosition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wind")
        {
            WindActive windActive = other.GetComponent<WindActive>();
            if (windActive.windSpeed > 4)
                Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    { 
        Debug.Log(other.transform.gameObject);
        if (other.gameObject.tag == "Wind")
        {
            gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, 2, other.gameObject.transform.position.z);
            Debug.Log("entered");
        }
    }
}
