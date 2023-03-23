using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Breeze : MonoBehaviour
{
    private Wind wind;
    bool carryState = false;
    bool destroyState = false;
    FixedJoint gameObjectJoint;
    Collider goCollided;

    private void OnEnable()
    {
        wind = GetComponentInParent<GetWind>().GetWindObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        tryGettingComponents(other);
    }

    private void tryGettingComponents(Collider other)
    {
        carryState = other.gameObject.TryGetComponent<Carry>(out Carry carry);
        destroyState = other.gameObject.TryGetComponent<Remove>(out Remove remove);
        checkCollisions(other);
    }

    private void checkCollisions(Collider other)
    {
        other.TryGetComponent<FixedJoint>(out var joint);
        if (joint == null)
        {
            if (carryState == true)
            {
                wind.OnWindFinished.AddListener(breakConnection);
                gameObjectJoint = other.gameObject.AddComponent<FixedJoint>();
                gameObjectJoint.connectedBody = gameObject.GetComponent<Rigidbody>();
            }
        }


        else if (destroyState)
            return;

        else
            return;
    }

    void breakConnection()
    {
        if (gameObjectJoint != null)
        {
            Destroy(gameObjectJoint);
        }
    }
}
