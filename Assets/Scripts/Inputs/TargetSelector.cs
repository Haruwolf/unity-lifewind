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

    GameObject cloudCreated;

    public int managerCount;

    public enum createStates
    {
        Clouding,
        Winding,
        None
    }

    createStates actualState = createStates.None;

    public GameObject cloudPrefab;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                if (hitInfo.collider != null)
                {
                    if (hitInfo.collider.gameObject.tag == "Grass")
                        CreateWind();

                    if (hitInfo.collider.gameObject.tag == "Water")
                        CreateCloud(hitInfo.collider.gameObject);
                }
            }

        }

        if (Input.GetMouseButton(0))
        {
            timePressed += Time.deltaTime;
            if (timePressed > maximumPressTime)
            {
                if (actualState == createStates.Winding)
                    ChargeWind();

                if (actualState == createStates.Clouding)
                    ChargeCloud();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (actualState == createStates.Winding)
            {
                ReleaseWind();
                CancelWind();
            }

            if (actualState == createStates.Clouding)
                ReleaseCloud();

        }


    }

    void CreateWind()
    {
        actualState = createStates.Winding;
        if (managerCount < 4)
        {
            if (Input.GetMouseButtonDown(0))
            {
                managerCount = Mathf.Clamp(managerCount++, 0, 4);
                managerClone = Instantiate(windManager.gameObject);
                BlockSelected();
            }


        }
    }

    void CreateCloud(GameObject actualBlock)
    {
        actualState = createStates.Clouding;
        cloudCreated = Instantiate(cloudPrefab.gameObject, new Vector3(actualBlock.transform.position.x, actualBlock.transform.position.y + 5, actualBlock.transform.position.z), cloudPrefab.transform.rotation);
        Debug.Log(actualState);
    }

    void ChargeCloud()
    {
        cloudCreated.GetComponent<Cloud>().cloudStateActual = Cloud.cloudState.Holding;
        cloudCreated.GetComponent<Cloud>().fillCloudHP();
        Debug.Log("charging cloud");
    }

    void ReleaseCloud()
    {
        cloudCreated.GetComponent<Cloud>().cloudStateActual = Cloud.cloudState.Released;
        actualState = createStates.None;
        cloudCreated = null;

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
        managerClone.GetComponent<WindManager>().releaseWind();
        managerCount = Mathf.Clamp(managerCount - 1, 0, 4);

        actualState = createStates.None;
    }

    private void CancelWind()
    {
        managerClone.GetComponent<WindManager>().canceledWind();
    }
}
