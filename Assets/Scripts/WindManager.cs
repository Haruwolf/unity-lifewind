using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public GameObject cyclonePrefab;
    public GameObject breezePrefab;
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

    public WindActive windActive;

    [SerializeField]
    [Range(0f, 10f)]
    float windHeight = 7.5f;

    GameObject cyClone;
    GameObject breezeClone;



    private void Start()
    {
        cycloneParticle = cyclonePrefab.GetComponent<ParticleSystem>();
        breezeParticle = breezePrefab.GetComponent<ParticleSystem>();

        cycloneParticle.Stop();
        breezeParticle.Stop();
    }

    public void Update()
    {

    }

    public void setWindPos(GameObject actualBlock)
    {
        cyClone = Instantiate(cyclonePrefab, new Vector3(actualBlock.transform.position.x, actualBlock.transform.position.y + windHeight, actualBlock.transform.position.z), cycloneParticle.transform.rotation);
        cyClone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        cyClone.GetComponent<ParticleSystem>().Play();
        startDirection = cyClone.transform.position;
        startDirectionstatic = startDirection;

    }

    public void chargeWind(GameObject actualBlock)
    {
        cyClone.transform.position = new Vector3(actualBlock.transform.position.x, actualBlock.transform.position.y + windHeight, actualBlock.transform.position.z);
        holdDirection = cyClone.transform.position;
        float actualDistance = Mathf.Clamp(Vector3.Distance(holdDirection, startDirection) / 10, 0.5f, 3.0f);
        cyClone.transform.localScale = new Vector3(actualDistance, actualDistance, actualDistance);

    }

    public void releaseWind(GameObject actualBlock)
    {
        cyClone.GetComponent<ParticleSystem>().Stop();
        endDirection = cyClone.transform.position;
        endDirectionstatic = endDirection;
        breezeClone = Instantiate(breezePrefab, endDirection, breezePrefab.transform.rotation);
        breezeClone.GetComponent<ParticleSystem>().Play();
        windTimeLife = 0;
        Rigidbody breezeRigidbody = breezeClone.GetComponent<Rigidbody>();
        breezeRigidbody.AddForce((startDirection - endDirection).normalized * speedLaunch, ForceMode.VelocityChange);
        setWindTimer();

    }

    public void canceledWind()
    {
        cyClone.GetComponent<ParticleSystem>().Stop();
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
            breezeClone.GetComponent<ParticleSystem>().Stop();
            Wind.ActualState = Wind.windState.None;
            //Destroy(breezeClone);
            //Destroy(cyClone);

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
