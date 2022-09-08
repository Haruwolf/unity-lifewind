using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindActive : MonoBehaviour
{
    public float windSpeed;
    public Wind wind;

    public void OnEnable()
    {
        wind = new Wind();
    }
    public void updateState(Wind.windState actualState)
    {
        this.wind = wind;
        wind.ActualState = actualState;
        Debug.Log(wind.ActualState);
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(wind.ActualState);
        if (other.gameObject.tag == "Weed")
            if (windSpeed > 2 && wind.ActualState == Wind.windState.Released)
                Destroy(other.gameObject);
    }



}
