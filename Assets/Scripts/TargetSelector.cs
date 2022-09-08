using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 0.5f)]
    [Header("Press Times")]
    float minimumPressTime = 0.5f;

    [SerializeField]
    [Range(0f, 1f)]
    float maximumPressTime = 0.8f;


    public WindManager windManager;
    float timePressed;
    public WindActive windPrefab;
    Wind wind = new Wind();

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Wind.ActualState == Wind.windState.None)
            {
                BlockSelected();
            }
        }

        if (Input.GetMouseButton(0))
        {
            
                timePressed += Time.deltaTime;
                if (timePressed > maximumPressTime)
                {
                    Wind.ActualState = Wind.windState.Charging;
                    ChargeWind();
                }

        }

        if (Input.GetMouseButtonUp(0))
        {
            timePressed = 0;

            if (Wind.ActualState == Wind.windState.Charging)
            {
                Wind.ActualState = Wind.windState.Released;
                ReleaseWind();
            }

            if (Wind.ActualState == Wind.windState.None)
            {
                CancelWind();
            }

        }
    }

    private void BlockSelected()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.gameObject != null)
            {
                GameObject selectedTerrain = hitInfo.collider.gameObject;
                windManager.setWindPos(selectedTerrain);
            }
        }


    }

    private void ChargeWind()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.gameObject != null)
            {
                GameObject selectedTerrain = hitInfo.collider.gameObject;
                windManager.chargeWind(selectedTerrain);
            }
        }
    }

    private void ReleaseWind()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.gameObject != null)
            {
                GameObject selectedTerrain = hitInfo.collider.gameObject;
                windManager.releaseWind(selectedTerrain);
            }
        }
    }

    private void CancelWind()
    {
        windManager.canceledWind();
    }
}
