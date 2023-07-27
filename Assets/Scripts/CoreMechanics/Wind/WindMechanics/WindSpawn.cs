using UnityEngine;
using UnityEngine.Events;

namespace CoreMechanics.Wind
{
    public class WindSpawn : MonoBehaviour
    {
        [SerializeField] private WindPoolInitialize pool;
        private UnityEvent<WindStart> onWindCreated;
        private WindStart windStart;
        public void SetWind(Vector3 mousePos)
        {
            GameObject block = GetBlockSelection(mousePos);
            if (block == null)
            {
                return;
            }

            windStart = pool.WindPool.Get();
            if (windStart == null)
            {
                return;
            }
            
            windStart.transform.position = block.transform.position;
            OnWindCreated(windStart);
            //T0DO: Registrar o evento para arrastar no WindCreated
        }
        
        private void OnWindCreated(WindStart createdWind)
        {
            onWindCreated?.Invoke(createdWind);
        }
        
        private GameObject GetBlockSelection(Vector3 mousePos)
        {
            var mousePosConverted = Camera.main!.ScreenPointToRay(mousePos);
            if (Physics.Raycast(mousePosConverted, out RaycastHit hitInfo, Mathf.Infinity))
            {
                return hitInfo.transform.gameObject;
            }
            return null;
        }

    }
    
}



