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
        [SerializeField] private WindStartSettings windSettings;
        
        private IObjectPool<WindStart> windPool;
        private WindHandler _windHandler;
        
        public void AddControllerEvents()
        {
            gameObject.transform.parent.TryGetComponent(out WindControl windController);
            if (windController == null)
            {
                return;
            }
            
            windController.holdWind.AddListener(WindHandler.HoldWind);
            windController.releaseWind.AddListener(WindHandler.DeactivateWind);
        }

        public void RemoveControllerEvents()
        {
            gameObject.transform.parent.TryGetComponent(out WindControl windController);
            if (windController == null)
            {
                return;
            }
            windController.holdWind.RemoveListener(WindHandler.HoldWind);
            windController.releaseWind.RemoveListener(WindHandler.DeactivateWind);
        }

        private void OnEnable()
        {
            TryGetComponent(out ParticleSystem particles);
            _windHandler = new WindHandler(this, particles);
        }
        public IObjectPool<WindStart> WindPool
        {
            get => windPool;
            set => windPool = value;
        }

        public WindStartSettings WindSettings => windSettings;

        public WindHandler WindHandler => _windHandler;
        
        //Precisa estar na classe MonoBehaviour que a partícula está atrelada
        //TODO: Reportar a Unity
        private void OnParticleSystemStopped()
        {
            Debug.Log("Called");
            WindPool.Release(this);
        }
    }
    
    public class WindHandler
    {
        private float windActualSpeed;
        private ParticleSystem particleSystem;
        private WindStart _windStart;
        private WindStartSettings windSettings;
        private Vector3 lastValidPos;
        public float HoldTime { get; private set; }
        public Vector3 OriginalPos { get; private set; }
        public Vector3 DirectionAim { get; private set; }

        public WindHandler(WindStart windStart, ParticleSystem particleSystem)
        {
            _windStart = windStart;
            windSettings = _windStart.WindSettings;
            windActualSpeed = windSettings.windMaxSpeed;
            this.particleSystem = particleSystem;
            this.particleSystem.Play(); 
            SetParticleCallback();
        }
        
        public void HoldWind(Vector3 pos)
        {
            OriginalPos = _windStart.transform.position;
            var mousePosConverted = Camera.main!.ScreenPointToRay(pos);
            
            if (Physics.Raycast(mousePosConverted, out RaycastHit hitInfo, Mathf.Infinity))
            {
                lastValidPos = hitInfo.point;
            }

            Vector3 targetPos = lastValidPos;
            windActualSpeed = UpdateWindSpeed();
            //UpdateWindSize(); Colocar isso na próxima sprint.
            var throwDirection = (OriginalPos - targetPos).normalized;
            throwDirection.y = 0.5f;
            Debug.DrawRay(targetPos, throwDirection, Color.red);
            _windStart.gameObject.transform.position = Vector3.Lerp
            (
                a: _windStart.gameObject.transform.position, 
                b: targetPos, 
                t: windActualSpeed * Time.fixedDeltaTime
            );

            HoldTime += Time.deltaTime;
        }

        /*private void UpdateWindSize()
        {
            //TODO: Colocar tudo isso aqui no construtor, ver a melhor forma da particula crescer
            //TODO: A melhor forma de fazer isso é na verdade colocar dentro de um foreach e clampar até onde pode crescer cada particula, ajustar nas settings.
            Vector3 newSize = new Vector3(
                Mathf.Clamp(_windStart.transform.localScale.x, windSettings.windMinSize, windSettings.windMaxSize),
                Mathf.Clamp(_windStart.transform.localScale.y, windSettings.windMinSize, windSettings.windMaxSize),
                Mathf.Clamp(_windStart.transform.localScale.z, windSettings.windMinSize, windSettings.windMaxSize));
            newSize += new Vector3(windSettings.windSizeRate, windSettings.windSizeRate, windSettings.windSizeRate) *Time.deltaTime;
            _windStart.transform.localScale = newSize;
        }*/

        private float UpdateWindSpeed()
        {
            windActualSpeed -= windSettings.windDecreateRate * Time.fixedDeltaTime;
            windActualSpeed = Mathf.Clamp
            (
                value: windActualSpeed, 
                min: windSettings.windMinSpeed, 
                max: windSettings.windMaxSpeed
            );
            return windActualSpeed;
        }

        public void DeactivateWind()
        {
            _windStart.RemoveControllerEvents();
            _windStart.StartCoroutine(WindDisableRoutine());
        }
        
        private IEnumerator WindDisableRoutine()
        {
            yield return new WaitForSeconds(1.0f);
            particleSystem.Stop();
        }

        private void SetParticleCallback()
        {
            var main = particleSystem.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        //Classes de callback de particulas precisam ser públicas.
        
    }
}

