using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace CoreMechanics.Wind
{
    public class WindProjectile : MonoBehaviour
    {
        [SerializeField] private float windDespawnTime = 4f;
        [SerializeField] private ParticleSystem windParticles;

        private IObjectPool<WindProjectile> windPool;
        public IObjectPool<WindProjectile> WindPool
        {
            set => windPool = value;
        }

        private void OnEnable()
        {
            windParticles.Play();
        }

        public void DeactivateWind()
        {
            StartCoroutine(WindDisableRoutine(windDespawnTime));
        }

        private IEnumerator WindDisableRoutine(float despawnTime)
        {
            yield return new WaitForSeconds(despawnTime);
            //Finalizar depois
        }
    }
}

