using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlantController))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlantCollisionConfig : MonoBehaviour
{
    BoxCollider colliderGameObject;
    Rigidbody rigidbodyGameObject;
    void Awake()
    {
        GetComponents();
        InitializeBoxCollider();
        InitializeRigidbody();
    }

    void GetComponents()
    {
        colliderGameObject = GetComponent<BoxCollider>();
        rigidbodyGameObject = GetComponent<Rigidbody>();
    }

    void InitializeBoxCollider()
    {
        colliderGameObject.isTrigger = true;
    }

    void InitializeRigidbody()
    {
        rigidbodyGameObject.mass = 0.01f;
        rigidbodyGameObject.useGravity = false;
        rigidbodyGameObject.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | 
        RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
}
