using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CoreMechanics.Wind
{
    public class WindControl : MonoBehaviour
    {
        public UnityEvent<Vector3> createWind;
        
        [HideInInspector]
        public UnityEvent<Vector3> holdWind;
        
        [HideInInspector]
        public UnityEvent releaseWind;
   
        public void OnCreateWind(Vector3 windPos)
        {
            createWind?.Invoke(windPos);
        }

        public void OnHoldingWind(Vector3 windPos)
        {
            holdWind?.Invoke(windPos);
        }

        public void OnReleasedWind()
        {
            releaseWind?.Invoke();
        }
    }
}

