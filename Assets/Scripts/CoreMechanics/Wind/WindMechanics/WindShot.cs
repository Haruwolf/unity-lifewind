using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreMechanics.Wind
{
    public class WindShot : MonoBehaviour
    {
        private void ShotWind()
        {
            TryGetComponent(out WindStart windStart);
            float windForce = windStart.WindHandler.HoldTime;
            Debug.Log(windForce);
        }        
        private void OnEnable()
        {
            TryInitialize();
        }
        private void TryInitialize()
        {
            if (gameObject.transform.parent == null)
            {
                Invoke(nameof(TryInitialize), 0.1f);
                return;
            }

            AddReleaseEvent();
        }

        private void AddReleaseEvent()
        {
            gameObject.transform.parent.TryGetComponent(out WindControl windController);
            if (windController == null)
            {
                return;
            }
            
            windController.releaseWind.AddListener(ShotWind);
        }

        

        
    }
}

