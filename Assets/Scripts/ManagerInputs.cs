using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ManagerInputs : MonoBehaviour
{
    private TouchControls touchControls;

    public delegate void StartTouchEvent(Vector2 pos);
    public static event StartTouchEvent onStartTouch;
    public delegate void EndTouchEvent(Vector2 pos);
    public static event EndTouchEvent onEndTouch;
    public delegate void HoldTouchEvent(Vector2 pos);
    public static event HoldTouchEvent onHoldTouch;
    public delegate void ReleaseTouchEvent();
    public static event ReleaseTouchEvent onReleaseTouch;

    private void Awake()
    {
        touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        touchControls.Enable();
        onHoldTouch += BlockSelected;
    }

    private void OnDisable()
    {
        touchControls.Disable();
        onHoldTouch -= BlockSelected;
    }

    private void Start()
    {
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
        touchControls.Touch.TouchDrag.performed += ctx => HoldTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => ReleaseTouch(ctx);
        

    }

    private void StartTouch(InputAction.CallbackContext ctx)
    {
       //Checar o porque precisa clicar mais de uma vez;
        Vector2 touchPos = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        if (onStartTouch != null) onStartTouch(touchPos);
            WindManager.instance.startDirection = touchPos;


    }

    private void EndTouch(InputAction.CallbackContext ctx)
    {
        //Debug.Log(touchControls.Touch.TouchPosition);
        if (onEndTouch != null) onEndTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
    }

    private void HoldTouch(InputAction.CallbackContext ctx)
    {
        Vector3 touchPos = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        onHoldTouch(touchPos);
        

    }

    private void ReleaseTouch(InputAction.CallbackContext ctx)
    {
        WindManager.instance.releaseWind();

    }

    private void BlockSelected(Vector2 touchPos)
    {

        Ray ray = Camera.main.ScreenPointToRay(touchPos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.gameObject != null)
            {
                GameObject selectedTerrain = hitInfo.collider.gameObject;
                WindManager.instance.endDirection = touchPos;
                WindManager.instance.chargeWind(selectedTerrain);
            }
        }

        
    }
}
