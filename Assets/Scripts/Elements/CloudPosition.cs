using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPosition : MonoBehaviour
{
    public GameObject windManager;
    Vector3 force;

    private void Start()
    {

    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Wind" && other.gameObject.GetComponentInParent<WindActive>().wind.ActualState == Wind.windState.Released)
    //    {
    //        force = Vector3.zero;
    //        force = WindManager.startDirectionstatic - WindManager.endDirectionstatic;
    //        gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
    //        Debug.Log("entered");
    //    }
    //}


}
