using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlantCollisionConfig : MonoBehaviour
{
    BoxCollider collider;
    Rigidbody rigidbody;
    void Awake()
    {
        GetComponents();
        InitializeBoxCollider();
        InitializeRigidbody();
    }

    void GetComponents()
    {
        collider = GetComponent<BoxCollider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void InitializeBoxCollider()
    {
        collider.isTrigger = true;
    }

    void InitializeRigidbody()
    {
        rigidbody.mass = 0.01f;
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | 
        RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
}
