using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    public static TargetSelector instance;
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
    GameObject actualGameBlockCloud;

    public int managerCount;
    public int cloudCount;

    public enum createStates
    {
        Clouding,
        Winding,
        None
    }

    createStates actualState = createStates.None;

    public GameObject cloudPrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
                {
                    if (hitInfo.collider != null)
                    {
                        if (hitInfo.collider.gameObject.tag == "Grass")
                            if (managerCount < 3)
                                CreateWind();

                        if (hitInfo.collider.gameObject.tag == "Water")
                            if (cloudCount < 2)
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
        actualGameBlockCloud = actualBlock;
        cloudCount++;
        actualState = createStates.Clouding;
        cloudCreated = Instantiate(cloudPrefab.gameObject, new Vector3(actualBlock.transform.position.x, actualBlock.transform.position.y + 7.5f, actualBlock.transform.position.z), cloudPrefab.transform.rotation);
        Debug.Log(actualState);
    }

    void ChargeCloud()
    {
        if (cloudCreated != null)
        {
            cloudCreated.GetComponent<Cloud>().cloudStateActual = Cloud.cloudState.Holding;
            cloudCreated.GetComponent<Cloud>().fillCloudHP(actualGameBlockCloud);
            Debug.Log("charging cloud");
        }
    }

    void ReleaseCloud()
    {
        if (cloudCreated != null)
        {
            cloudCreated.GetComponent<Cloud>().cloudStateActual = Cloud.cloudState.Released;
            actualState = createStates.None;
            cloudCreated = null;
        }
        actualGameBlockCloud = null;

    }

    private void BlockSelected()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.gameObject != null)
            {
                GameObject selectedTerrain = hitInfo.collider.gameObject;
                managerClone.GetComponent<WindManager>().CreateWindPrefab(selectedTerrain);
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
                managerClone.GetComponent<WindManager>().SetWindCharge(selectedTerrain);
            }
        }
    }

    private void ReleaseWind()
    {
        managerClone.GetComponent<WindManager>().SetReleaseWindState();
        managerCount = Mathf.Clamp(managerCount - 1, 0, 4);

        actualState = createStates.None;
    }

    private void CancelWind()
    {
        managerClone.GetComponent<WindManager>().CancelWind();
    }
}
