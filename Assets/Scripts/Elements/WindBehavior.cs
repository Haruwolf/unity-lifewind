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
        other.TryGetComponent<FixedJoint>(out var joint);
        if (carryState && joint == null)
        {
            gameObjectJoint = other.gameObject.AddComponent<FixedJoint>();
            gameObjectJoint.connectedBody = gameObject.GetComponent<Rigidbody>();

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

            WindManager.windEvent -= breakConnection;
        }
    }
}
