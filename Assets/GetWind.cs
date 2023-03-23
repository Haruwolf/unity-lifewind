using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWind : MonoBehaviour
{
    Wind wind;

    [HideInInspector]
    public Wind Wind;
    // Start is called before the first frame update
    void OnEnable()
    {
        wind = gameObject.GetComponentInParent<Wind>()?.GetWind();
        Debug.Log(gameObject.transform.parent.name);
        Wind = wind;
    }

    public Wind GetWindObject()
    {
        return Wind;
    }

}
