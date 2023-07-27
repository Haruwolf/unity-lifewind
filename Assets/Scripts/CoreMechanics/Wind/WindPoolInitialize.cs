using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace CoreMechanics.Wind
{
    public class WindPoolInitialize : MonoBehaviour
    {
        [SerializeField] private WindPoolSettings windPoolSettings;
        public IObjectPool<WindStart> WindPool { get; private set; }
        private void CreatePool()
        {
            WindPool = new ObjectPool<WindStart>
            (
                createFunc: CreateWindStart, 
                actionOnGet: OnGetPool, 
                actionOnRelease: OnReleasePool, 
                actionOnDestroy: OnDestroyPool, 
                windPoolSettings.collectionCheck, 
                windPoolSettings.defaultPoolCapacity, 
                windPoolSettings.maxPoolSize
            );
        }
        
        private WindStart CreateWindStart()
        {
            WindStart windInstance = Instantiate(windPoolSettings.windStart);
            windInstance.WindPool = WindPool;
            windInstance.name = windPoolSettings.windCloneName;
            return windInstance;
        }

        private void OnReleasePool(WindStart windGameObject)
        {
            windGameObject.gameObject.SetActive(false);
        }

        private void OnGetPool(WindStart windGameObject)
        {
            windGameObject.gameObject.SetActive(true);
        }

        private void OnDestroyPool(WindStart windGameObject)
        {
            Destroy(windGameObject.gameObject);
        }
        
        private void Start()
        {
            CreatePool();
        }
        
    }
    
}

