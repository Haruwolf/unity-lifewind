using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchStates : MonoBehaviour
{
   public static TouchStates instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (instance != null || instance != this)
            Destroy(this);
    }
    public enum touchStates
    {
        Free,
        Selected,
        Holding,
        Released,
    }

    public touchStates actualState = touchStates.Free;
}
