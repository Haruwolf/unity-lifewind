using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlantController))]
public class PlantOriginalPos : MonoBehaviour
{
    private Vector3 _originalPos;

    [HideInInspector]
    public Vector3 OriginalPos {
        get {return _originalPos;}
        set {
            _originalPos = value;
        }
    }

    public Vector3 GetOriginalPos()
    {
        return OriginalPos;
    }
}
