using UnityEngine;
using GameSystem.Inputs;

namespace GameSystem.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;
        private static Player SingletonInstance { get; set; }
        
        private void Awake()
        {
            if (SingletonInstance != null & SingletonInstance != this)
            {
                Destroy(gameObject);
            }

            else
            {
                SingletonInstance = this;
            }
            
            DontDestroyOnLoad(gameObject);
            SetComponentsActive();
        }
        
        private void SetComponentsActive()
        {
            inputManager.enabled = true;
        }
    }
}

