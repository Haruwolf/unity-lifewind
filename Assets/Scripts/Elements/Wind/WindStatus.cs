using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindStatus : MonoBehaviour
{
    public WindObject wind;
    public string windStatus;


    public void OnEnable()
    {
        wind = new WindObject();
    }
    public void updateState(WindObject.windState actualState)
    {
        wind.ActualState = actualState;
        windStatus = actualState.ToString();
    }

   



}
