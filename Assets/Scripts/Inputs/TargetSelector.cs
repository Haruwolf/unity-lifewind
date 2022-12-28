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
    GameObject waterBlockSelected;
    GameObject actualGameBlockCloud;

    public int windCount;
    public int windMaxCount;
    public int cloudCount;
    public int cloudMaxCount;
    public float cloudHeight;



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

    public delegate void ObserverStates();
    public static ObserverStates stateObserver;



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
                        hitInfo.collider.gameObject.TryGetComponent<Grass>(out Grass grass);
                        hitInfo.collider.gameObject.TryGetComponent<Water>(out Water water);
                        if (grass)
                            if (windCount < windMaxCount)
                                CreateWind();

                        if (water)
                            if (cloudCount < cloudMaxCount)
                            {
                                CreateCloud(hitInfo.collider.gameObject);
                                waterBlockSelected = hitInfo.collider.gameObject;
                            }
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
                        ChargeCloudUseWater();
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
        if (windCount < 4)
        {
            if (Input.GetMouseButtonDown(0))
            {
                windCount = Mathf.Clamp(windCount++, 0, 4);
                managerClone = Instantiate(windManager.gameObject);
                BlockSelected();
            }


        }
    }

    void CreateCloud(GameObject blockSelected)
    {
        actualGameBlockCloud = blockSelected;
        cloudCount++;
        actualState = createStates.Clouding;
        cloudCreated = Instantiate(cloudPrefab.gameObject, new Vector3(blockSelected.transform.position.x, blockSelected.transform.position.y + cloudHeight, blockSelected.transform.position.z), cloudPrefab.transform.rotation);
    }

    void ChargeCloudUseWater()
    {
        if (cloudCreated != null && waterBlockSelected != null)
        {
            waterBlockSelected.TryGetComponent<Water>(out Water water);
            cloudCreated.TryGetComponent<Cloud>(out Cloud cloud);
            if (cloud)
            {
                cloud.cloudStateActual = Cloud.cloudState.Holding;
                water.createCloudUsingWater(cloud, actualGameBlockCloud);

            }
        }
    }

    void ReleaseCloud()
    {
        if (cloudCreated != null)
        {
            cloudCreated.GetComponent<Cloud>().cloudStateActual = Cloud.cloudState.Released;
            actualState = createStates.None;
            cloudCreated = null;
            waterBlockSelected.TryGetComponent<Water>(out Water water);
            water.rechargeRiver();
        }
        actualGameBlockCloud = null;
        waterBlockSelected = null;
        stateObserver();

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
        windCount = Mathf.Clamp(windCount - 1, 0, 4);

        actualState = createStates.None;
    }

    private void CancelWind()
    {
        managerClone.GetComponent<WindManager>().CancelWind();
    }
}
