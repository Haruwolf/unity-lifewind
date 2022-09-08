using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public GameObject cyclonePrefab;
    public GameObject breezePrefab;
    public GameObject windParentPrefab;
    ParticleSystem cycloneParticle;
    ParticleSystem breezeParticle;
    public float speedLaunch;

    public Vector3 startDirection;
    public Vector3 endDirection;
    public Vector3 holdDirection;

    public static Vector3 startDirectionstatic;
    public static Vector3 endDirectionstatic;


    public Ray origin;
    public Vector3 direction;

    float windTimeLife;


    [SerializeField]
    [Range(0f, 10f)]
    float windHeight = 7.5f;

    GameObject cyClone;
    GameObject breezeClone;
    GameObject windClone;



    private void OnEnable()
    {
        cycloneParticle = cyclonePrefab.GetComponent<ParticleSystem>();
        breezeParticle = breezePrefab.GetComponent<ParticleSystem>();

        cycloneParticle.Stop();
        breezeParticle.Stop();
    }

    public void setWindPos(GameObject actualBlock)
    {
        windClone = Instantiate(windParentPrefab, new Vector3(actualBlock.transform.position.x, actualBlock.transform.position.y + windHeight, actualBlock.transform.position.z), cycloneParticle.transform.rotation);
        cyClone = windClone.transform.GetChild(0).gameObject;
        cyClone.SetActive(true);
        cyClone.transform.position = new Vector3(actualBlock.transform.position.x, actualBlock.transform.position.y + windHeight, actualBlock.transform.position.z);
        cyClone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        cyClone.GetComponent<ParticleSystem>().Play();
        windClone.GetComponent<WindActive>().updateState(Wind.windState.Setted);
        startDirection = cyClone.transform.position;
        startDirectionstatic = startDirection;

    }

    public void chargeWind(GameObject actualBlock)
    {
        if (windClone.GetComponent<WindActive>().wind.ActualState == Wind.windState.Setted || windClone.GetComponent<WindActive>().wind.ActualState == Wind.windState.Charging)
        {

            windClone.GetComponent<WindActive>().updateState(Wind.windState.Charging);
            cyClone.transform.position = new Vector3(actualBlock.transform.position.x, actualBlock.transform.position.y + windHeight, actualBlock.transform.position.z);
            holdDirection = cyClone.transform.position;
            float actualDistance = Mathf.Clamp(Vector3.Distance(holdDirection, startDirection) / 10, 0.5f, 3.0f);
            cyClone.transform.localScale = new Vector3(actualDistance, actualDistance, actualDistance);
            LineRenderer line = gameObject.GetComponent<LineRenderer>();
            line.SetPosition(0, startDirection);
            line.SetPosition(1, holdDirection);
            //Debug.DrawLine(startDirection, holdDirection, Color.black, 5, false);
        }

    }

    public void releaseWind(GameObject actualBlock)
    {
        if (windClone.GetComponent<WindActive>().wind.ActualState == Wind.windState.Charging)
        {
            windClone.GetComponent<WindActive>().updateState(Wind.windState.Released);
            
            endDirection = cyClone.transform.position;
            endDirectionstatic = endDirection;
            breezeClone = windClone.transform.GetChild(1).gameObject;
            breezeClone.SetActive(true);
            breezeClone.transform.position = endDirection;
            breezeClone.GetComponent<ParticleSystem>().Play();
            windTimeLife = 0;
            Rigidbody breezeRigidbody = breezeClone.GetComponent<Rigidbody>();
            breezeRigidbody.AddForce((startDirection - endDirection).normalized * speedLaunch, ForceMode.VelocityChange);
            setWindTimer();
        }
    }

    public void canceledWind()
    {
        cyClone.GetComponent<ParticleSystem>().Stop();
    }

    private void setWindTimer()
    {
        windTimeLife = Mathf.Clamp(Vector3.Distance(startDirection, endDirection), 2f, 6f);
        setWindProperty(windTimeLife);
        Invoke(nameof(startWindTimer), windTimeLife);
    }

    private void setWindProperty(float windTime)
    {
        windClone.GetComponent<WindActive>().windSpeed = windTime;
    }

    void startWindTimer()
    {
        breezeClone.GetComponent<ParticleSystem>().Stop();
        cyClone.GetComponent<ParticleSystem>().Stop();
        windClone.GetComponent<WindActive>().updateState(Wind.windState.None);
        Destroy(gameObject, 2);
        Destroy(windClone, 4);

    }

    //public void releaseWind(Vector3 endDirection)
    //{
    //    Debug.Log("Direção inicial" + startDirection);
    //    Debug.Log("Direção final" + endDirection);
    //    wind.ActualState = Wind.windState.Released;
    //    windPrefab.GetComponent<Rigidbody>().AddForce((startDirection - endDirection).normalized * speedLaunch, ForceMode.Impulse);

    //}



}
