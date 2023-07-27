using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace CoreMechanics.Wind
{
    public class WindStart : MonoBehaviour
    {
        [SerializeField] private ParticleSystem windParticles;
        private IObjectPool<WindStart> windPool;
        public IObjectPool<WindStart> WindPool
        {
            set => windPool = value;
        }

        private void OnEnable()
        {
            windParticles.Play();
            SetParticleCallback();
            DeactivateWind();
        }
        
        private void SetParticleCallback()
        {
            var main = windParticles.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }
        
        public void OnParticleSystemStopped()
        {
            Debug.Log("Stop");
            windPool.Release(this);
        }

        private void DeactivateWind()
        {
            StartCoroutine(WindDisableRoutine());
        }

        private IEnumerator WindDisableRoutine()
        {
            yield return new WaitForSeconds(1.0f);
            windParticles.Stop();
        }
    }
}

