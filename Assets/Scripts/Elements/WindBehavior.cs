using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WindBehavior : MonoBehaviour
{
    bool carryState = false;
    bool destroyState = false;
    FixedJoint gameObjectJoint;
    Collider goCollided;

    private void OnTriggerEnter(Collider other)
    {
        tryGettingComponents(other);
        WindManager.windEvent += breakConnection;
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
            gameObjectJoint = other.gameObject.AddComponent<FixedJoint>();
            gameObjectJoint.connectedBody = gameObject.GetComponent<Rigidbody>();
        }
            

        else if (destroyState)
            return;
    }

    void breakConnection()
    {
        if (gameObjectJoint != null)
        {
            gameObjectJoint = null;

            WindManager.windEvent -= breakConnection;
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    carryState = false;
    //    destroyState = false;
    //}
}
