using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowIndicator : MonoBehaviour
{
    private void OnEnable()
    {
        Wind.ToggleArrow += ToggleArrow;


    }
    private void OnDisable()
    {
        Wind.ToggleArrow -= ToggleArrow;
    }
    void ToggleArrow(Vector3 startDir, Vector3 holdDir, GameObject go, bool toggle)
    {
        if (toggle)
        {
            LineRenderer line = go.GetComponent<LineRenderer>();
            line.SetPosition(0, startDir);
            line.SetPosition(1, holdDir);
        }

        else
        {
            go.GetComponent<LineRenderer>().enabled = false;
        }

    }

}
