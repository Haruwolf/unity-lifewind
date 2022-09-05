using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    public WindManager windManager;
    float timePressed;
    Wind wind = new Wind();
    
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    BlockSelected();
        //}

        if (Input.GetMouseButton(0))
        {
            timePressed += Time.deltaTime;

            if (timePressed > 0.5f && timePressed < 0.8f)
                BlockSelected();

            else if (timePressed > 0.8f)
            {
                wind.ActualState = Wind.windState.Charging;
                ChargeWind();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            timePressed = 0;

            if (wind.ActualState == Wind.windState.Charging)
            {
                wind.ActualState = Wind.windState.None;
                ReleaseWind();
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
}
