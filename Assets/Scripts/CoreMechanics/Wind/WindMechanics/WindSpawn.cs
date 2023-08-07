using UnityEngine;
using UnityEngine.Events;

namespace CoreMechanics.Wind
{
    public class WindSpawn : MonoBehaviour
    {
        [SerializeField] private WindPoolInitialize pool;
        [SerializeField] private WindControl windController;
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

            var windStartTransform = windStart.transform;
            windStartTransform.position = block.transform.position;
            windStartTransform.parent = windController.transform;
            windStart.AddControllerEvents();
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



