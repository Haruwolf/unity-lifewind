using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WindBehavior : MonoBehaviour
{

    
    bool leavingCloud;
    bool carryState = false;
    bool destroyState = false;
    Collider goCollided;
    Vector3 collidedPoint;


    //Criar dois scripts de componentes, um chamado Carry e outro chamado Destroy

    private void OnTriggerEnter(Collider other)
    {
        tryGettingComponents(other);

    }

    private void tryGettingComponents(Collider other)
    {

        carryState = other.gameObject.TryGetComponent<Carry>(out Carry carry);
        destroyState = other.gameObject.TryGetComponent<Remove>(out Remove remove);
        goCollided = other;
        checkCollisions(other);
    }

    private void checkCollisions(Collider other)
    {  
        if (carryState)
        {
            Debug.Log("Carrying");
            //gameObject.AddComponent<FixedJoint>().connectedBody = other.GetComponent<Rigidbody>();

            //FixedJoint: Você adiciona ao objeto que precisa se mover junto com o objeto que está se movimento, ele vai ficar "grudado" No caso, a nuvem.
            other.gameObject.AddComponent<FixedJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
        }

        //else if (destroyState)
        //{
        //    Debug.Log("Destroying");
        //}
    }

    //private void Update()
    //{
    //    if (carryState)
    //    {
    //        goCollided.gameObject.transform.position = collidedPoint;
    //    }
    //}
    private void OnTriggerExit(Collider other)
    {
        carryState = false;
        destroyState = false;
    }
}
