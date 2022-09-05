using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public GameObject windPrefab;
    GameObject actualBlock;
    Wind wind = new Wind();
    public float speedLaunch;

    Vector3 windForce = new Vector3();

    public Vector3 startDirection;
    public Vector3 endDirection;

    public Ray origin;
    public Vector3 direction;
    private Vector3 windDirection;

    float windTimeLife;

    public WindActive windActive;

   

    private void Start()
    {

    }

    public void Update()
    {

    }

    public void setWindPos(GameObject actualBlock)
    {

        //Vector3 temp = new Vector3();
        //temp.z += 0.1f;
        //windForce += temp;

        //Debug.Log(windForce);
        windPrefab.GetComponent<Rigidbody>().velocity = Vector3.zero;
        windPrefab.transform.position = new Vector3(actualBlock.transform.position.x, 1, actualBlock.transform.position.z);
        startDirection = windPrefab.transform.position;

        //Debug.Log(windDirection);


    }

    public void chargeWind(GameObject actualBlock)
    {
        windPrefab.transform.position = new Vector3(actualBlock.transform.position.x, 1, actualBlock.transform.position.z);
    }

    public void releaseWind(GameObject actualBlock)
    {
        windTimeLife = 0;
        endDirection = actualBlock.transform.position;
        windPrefab.transform.localEulerAngles = new Vector3(0, 45, 0);
        windPrefab.GetComponent<Rigidbody>().AddForce((startDirection - endDirection).normalized * speedLaunch, ForceMode.VelocityChange);
        setWindTimer();
        
    }

    private void setWindTimer()
    {
        windTimeLife = Mathf.Clamp(Vector3.Distance(startDirection, endDirection), 1f, 5f);
        setWindProperty(windTimeLife);
        StartCoroutine(startWindTimer(windTimeLife));
    }

    private void setWindProperty(float windTime)
    {
        windActive.GetComponent<WindActive>().windSpeed = windTime;
    }

    IEnumerator startWindTimer(float wTimer)
    {
        yield return new WaitForSeconds(wTimer);
        {
            windPrefab.transform.position = new Vector3(50, 50, 50);
        }
           
        
    }

    //public void releaseWind(Vector3 endDirection)
    //{
    //    Debug.Log("Direção inicial" + startDirection);
    //    Debug.Log("Direção final" + endDirection);
    //    wind.ActualState = Wind.windState.Released;
    //    windPrefab.GetComponent<Rigidbody>().AddForce((startDirection - endDirection).normalized * speedLaunch, ForceMode.Impulse);

    //}



}
