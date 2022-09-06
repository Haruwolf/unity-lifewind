using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindActive : MonoBehaviour
{
    public float windSpeed;
    public Wind wind = new Wind();

    public void updateState(Wind wind)
    {
        this.wind = wind;
        Debug.Log(Wind.ActualState);
    }



}
