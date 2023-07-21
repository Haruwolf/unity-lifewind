using Core.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Core
{
    public class InputManager : MonoBehaviour, IInputEvents
    {
        [SerializeField] private InputAction windAction;
        [SerializeField] private float holdTime;
        public UnityEvent holdingEvent;
        public UnityEvent performedEvent;
        public UnityEvent canceledEvent;
        
        private float HoldTime
        {
            get => holdTime; 
            set
            {
                if (value < 0.1f)
                {
                    Debug.Log($"Tempo de holding é muito curto, " +
                              $"mude para um valor maior em {gameObject.name}.");
                    holdTime = 0.1f;
                    return;
                }

                holdTime = value;
            }
        }
        
        private void Awake()
        {
            HoldTime = holdTime;
            InitiateActions();
        }
        
        private void InitiateActions()
        {
            windAction.performed += ctx =>
            {
                if (ctx.interaction is HoldInteraction)
                {
                    OnHold();
                }
            };
            
            windAction.canceled += ctx =>
            {
                if (ctx.duration < HoldTime)
                {
                    OnCanceled();
                }
                
                OnPerformed();
            };
        }

        private void OnEnable()
        {
            windAction.Enable();
        }
        
        public void OnHold()
        {
            holdingEvent.Invoke();
        }

        public void OnPerformed()
        {
            performedEvent.Invoke();
        }

        public void OnCanceled()
        {
            canceledEvent.Invoke();
        }
    }
}

