using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public GameObject windParentPrefab;

    public Vector3 startDirection;
    public Vector3 endDirection;
    public Vector3 holdDirection;

    [SerializeField]
    [Range(0f, 10f)]
    float windHeight = 7.5f;

    GameObject newCyclone;
    GameObject newBreeze;
    GameObject windClone;

    public ParticleSystem cycloneParticle;
    public ParticleSystem breezeParticle;
    public AudioSource windSound;

    WindActive windState;
    ParticleSystem newCycloneParticle;
    ParticleSystem newBreezeParticle;

    bool releasedWind;
    float windTimer;

    public float interpolationTime = 5;
    public float clearTime = 4;

    public delegate void windDelegateEvent();
    public static event windDelegateEvent windEvent;

   

    private void Update()
    {
        if (Time.deltaTime == 0)
        {
            if (windClone != null)
                if (windClone.GetComponent<WindActive>().wind.ActualState == Wind.windState.Charging)
                    releaseWind();
        }

        if (releasedWind)
        {
            windTimer += Time.deltaTime;
            newBreeze.transform.position = Vector3.Lerp(endDirection, startDirection, windTimer / interpolationTime);
            if (windTimer >= interpolationTime && windClone != null)
            {
                releasedWind = moveWind(false);
                stopWindParticles();
                clearWind();
                windEvent(); //Checar se quebrou a conexão, e tirar efetivamente o vento
                
            }
                

        }
    }

    public void setWindPos(GameObject actualBlock)
    {
        Vector3 windInitPos = calcWindPos(actualBlock.transform.position);
        windClone = Instantiate(windParentPrefab, windInitPos, cycloneParticle.transform.rotation);
        newCyclone = setCycloneState(windInitPos, windClone);
        windState = windClone.GetComponent<WindActive>();
        windState.updateState(Wind.windState.Setted);
        startDirection = newCyclone.transform.position; ;
    }

    GameObject setCycloneState(Vector3 windInitPos, GameObject windClone)
    {
        newCyclone = windClone.GetComponentInChildren<Cyclone>().gameObject;
        newCyclone.SetActive(true);
        newCyclone.transform.position = windInitPos; ;
        newCyclone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        newCycloneParticle = newCyclone.GetComponent<ParticleSystem>();
        newCycloneParticle.Play();
        return newCyclone;
    }

    private Vector3 calcWindPos(Vector3 blockPos)
    {
        return new Vector3(blockPos.x, blockPos.y + windHeight, blockPos.z);
    }

    public void chargeWind(GameObject actualBlock)
    {
        if (windState.wind.ActualState == Wind.windState.Setted || windState.wind.ActualState == Wind.windState.Charging)
        {
            chargeCyclone(actualBlock.transform.position);
            createArrow();
            setChargingWindSound();
        }

    }

    void createArrow()
    {
        LineRenderer line = gameObject.GetComponent<LineRenderer>();
        line.SetPosition(0, startDirection);
        line.SetPosition(1, holdDirection);
    }

    void chargeCyclone(Vector3 blockPos)
    {
        windClone.GetComponent<WindActive>().updateState(Wind.windState.Charging);
        newCyclone.transform.position = calcWindPos(blockPos);
        holdDirection = newCyclone.transform.position;
        float actualDistance = Mathf.Clamp(Vector3.Distance(holdDirection, startDirection) / 10, 0.5f, 3.0f);
        newCyclone.transform.localScale = new Vector3(actualDistance, actualDistance, actualDistance);
    }

    public void setChargingWindSound()
    {
        if (windSound.clip != (AudioClip)Resources.Load("VentoCarregando"))
        {
            windSound.clip = (AudioClip)Resources.Load("VentoCarregando");
            windSound.Play();
            windSound.loop = true;
        }
    }

    void toggleOffArrow()
    {
        gameObject.GetComponent<LineRenderer>().enabled = false;
    }

    public void releaseWind()
    {
        if (windState.wind.ActualState == Wind.windState.Charging)
        {
            toggleOffArrow();
            windState.updateState(Wind.windState.Released);
            endDirection = toggleOffCyclone();
            setNewBreeze();      
            releasedWind = moveWind(true);
            setReleaseWindSound();
        }
    }

    void setNewBreeze()
    {
        newBreeze = windClone.GetComponentInChildren<WindBehavior>().gameObject;
        newBreeze.SetActive(true);
        newBreeze.transform.position = endDirection;
        newBreeze.transform.localEulerAngles = new Vector3(0, 90, 0);
        newBreezeParticle = newBreeze.GetComponentInChildren<ParticleSystem>();
        newBreezeParticle.Play();
    }

    Vector3 toggleOffCyclone()
    {
        newCycloneParticle.Stop();
        newCyclone.GetComponent<ParticleSystem>().Stop();
        return newCyclone.transform.position;
    }

    public void setReleaseWindSound()
    {

        windSound.clip = (AudioClip)Resources.Load("VentoMovimento_Unity");
        windSound.Play();
        windSound.loop = true;
    }

    public void canceledWind()
    {
        if (startDirection == endDirection)
        {
            Destroy(windClone);
            windSound.Stop();
        }
    }
    private bool moveWind(bool state)
    {
        return state;
    }

    void stopWindParticles()
    {
        newBreezeParticle.Stop();
        windSound.loop = false;
    }

    void clearWind()
    {
        Destroy(windClone, clearTime);
        Destroy(gameObject, clearTime);
    }

}
