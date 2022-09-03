using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public static WindManager instance;
    public GameObject windPrefab;
    GameObject actualBlock;
    Wind wind = new Wind();
    public float speedLaunch;

    Vector3 windForce = new Vector3();

    public Vector3 startDirection;
    public Vector3 endDirection;

    private Vector3 windDirection;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (instance != null || instance != this)
            Destroy(this);
    }

    private void Start()
    {

    }

    public void chargeWind(GameObject actualBlock)
    {
        Vector3 temp = new Vector3();
        temp.z += 0.1f;
        windForce += temp;

        //Debug.Log(windForce);
        windPrefab.transform.position = new Vector3(actualBlock.transform.position.x, 1, actualBlock.transform.position.z);
        //Debug.Log(windPrefab.transform.position);
        wind.ActualState = Wind.windState.Charging;

        if (startDirection.x > endDirection.x)
            windDirection = Vector3.right;

        if (startDirection.x < endDirection.x)
            windDirection = Vector3.left;

        if(startDirection.y > endDirection.y)
            windDirection = Vector3.up;

        if (startDirection.y < endDirection.y)
            windDirection = Vector3.down;


        Debug.Log(windDirection);

        //Debug.Log("Direção inicial"+ startDirection);
        //Debug.Log("Direção final"+ endDirection);



    }

    public void releaseWind()
    {
        wind.ActualState = Wind.windState.Released;
        windPrefab.GetComponent<Rigidbody>().AddForce(windForce * Time.deltaTime * speedLaunch , ForceMode.Impulse);

    }



}
