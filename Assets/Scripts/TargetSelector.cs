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


    public GameObject windManager;
    float timePressed;
    public GameObject managerClone;

    public int managerCount;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (managerCount < 4)
        {
            if (Input.GetMouseButtonDown(0))
            {
                managerCount = Mathf.Clamp(managerCount++, 0, 4);
                managerClone = Instantiate(windManager.gameObject);
                BlockSelected();
            }

            if (Input.GetMouseButton(0))
            {
                timePressed += Time.deltaTime;
                if (timePressed > maximumPressTime)
                {
                    ChargeWind();
                }

            }

            if (Input.GetMouseButtonUp(0))
            {
                ReleaseWind();
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
                managerClone.GetComponent<WindManager>().setWindPos(selectedTerrain);
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
                managerClone.GetComponent<WindManager>().chargeWind(selectedTerrain);
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
                managerClone.GetComponent<WindManager>().releaseWind(selectedTerrain);
                managerCount = Mathf.Clamp(managerCount - 1, 0, 4);
            }
        }
    }

    private void CancelWind()
    {
        //windManager.canceledWind();
    }
}
