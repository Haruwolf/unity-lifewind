using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind
{
    // Start is called before the first frame update
    private Vector3 _chargeRate;

    public Vector3 ChargeRate { get { return _chargeRate; } set { _chargeRate = value; } }

    public enum windState
    {
        None,
        Charging,
        Released,
    }

    static windState _actualState = windState.None;

    public static windState ActualState { get { return _actualState; } set { _actualState = value; } }


}
