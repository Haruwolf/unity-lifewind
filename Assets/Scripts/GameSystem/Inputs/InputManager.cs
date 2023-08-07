using System;
using GameSystem.Inputs.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace GameSystem.Inputs
{
    public class InputManager : MonoBehaviour, IInputEvents
    {
        
        [SerializeField] private InputAction windAction;
        [SerializeField] private float holdTime;
        public UnityEvent<Vector3> startedEvent;
        public UnityEvent<Vector3> holdingEvent;
        public UnityEvent performedEvent;
        public UnityEvent canceledEvent;
        private bool isHolding = false;
        
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
            windAction.started += ctx =>
            {
                OnStarted();
            };
            
            windAction.performed += ctx =>
            {
                if (ctx.interaction is HoldInteraction)
                {
                    OnHold();
                }

                isHolding = true;
            };
            
            windAction.canceled += ctx =>
            {
                if (ctx.duration < HoldTime)
                {
                    OnCanceled();
                }
                
                OnPerformed();
                isHolding = false;
            };
        }

        private void FixedUpdate()
        {
            if (isHolding)
            {
                OnHold();
            }
        }

        private void OnEnable()
        {
            windAction.Enable();
        }

        public void OnStarted()
        {
            Vector3 mouseClickedPos = GetClickPos();
            startedEvent.Invoke(mouseClickedPos);
        }
        
        public void OnHold()
        {
            Vector3 mousePos = GetClickPos();
            holdingEvent.Invoke(mousePos);
        }

        public void OnPerformed()
        {
            performedEvent.Invoke();
        }

        public void OnCanceled()
        {
            canceledEvent.Invoke();
        }

        private Vector3 GetClickPos()
        {
            Vector3 mousePos = new Vector3(
                    Mouse.current.position.ReadValue().x, 
                    Mouse.current.position.ReadValue().y,
                    Camera.main!.transform.position.z);
                
            return mousePos;
        }
    }
}

