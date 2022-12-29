using JetBrains.Annotations;
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
    public AudioSource windAudioSource;

    WindActive windState;
    ParticleSystem newCycloneParticle;
    ParticleSystem newBreezeParticle;

    bool releasedWind;
    float windTimer;

    public float interpolationTime = 5;
    public float clearTime = 4;

    public delegate void windDelegateEvent();
    public static event windDelegateEvent windEvent;

    public delegate void arrowDelegateEvent(Vector3 startDir, Vector3 holdDir, GameObject windManager, bool toggle);
    public static event arrowDelegateEvent ToggleArrow;

    public delegate void soundDelegateEvent(AudioSource audio, string soundName);
    public static event soundDelegateEvent SoundEvent;

    public const string ventoCarregando = "ventoCarregando"; //const = static, ambos podem ser acessados globalmente
    public const string ventoSolto = "ventoSolto";
    public const string quebrarLoop = "quebrarLoop";



    private void Update()
    {
        if (Time.deltaTime == 0)
        {
            if (windClone != null)
                if (windClone.GetComponent<WindActive>().wind.ActualState == Wind.windState.Charging)
                    SetReleaseWindState();
        }

        if (releasedWind)
        {
            windTimer += Time.deltaTime;

            if (newBreeze != null)
            {

                newBreeze.transform.position = Vector3.Lerp(endDirection, startDirection, windTimer / interpolationTime);
                if (windTimer >= interpolationTime && windClone != null)
                {
                    releasedWind = WindReleasedState(false);
                    StopBreezeParticles();
                    ClearWind();
                    windEvent(); //Checar se quebrou a conexão, e tirar efetivamente o vento

                }
            }
                

        }
    }

    public void CreateWindPrefab(GameObject actualBlock)
    {
        Vector3 windInitPos = CalcWindPrefabPos(actualBlock.transform.position);
        windClone = Instantiate(windParentPrefab, windInitPos, cycloneParticle.transform.rotation);
        newCyclone = CreateNewCyclone(windInitPos, windClone);
        windState = windClone.GetComponent<WindActive>();
        windState.updateState(Wind.windState.Setted);
        startDirection = newCyclone.transform.position;
    }

    GameObject CreateNewCyclone(Vector3 windInitPos, GameObject windClone)
    {
        newCyclone = windClone.GetComponentInChildren<Cyclone>().gameObject;
        newCyclone.SetActive(true);
        newCyclone.transform.position = windInitPos; ;
        newCyclone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        newCycloneParticle = newCyclone.GetComponent<ParticleSystem>();
        newCycloneParticle.Play();
        return newCyclone;
    }

    public void SetWindCharge(GameObject actualBlock)
    {
        if (windState.wind.ActualState == Wind.windState.Setted || windState.wind.ActualState == Wind.windState.Charging)
        {
            ChargeCyclone(actualBlock.transform.position);
            ToggleArrow(startDirection, holdDirection, gameObject, true);
            SoundEvent(windAudioSource, ventoCarregando);
        }

    }

    void ChargeCyclone(Vector3 blockPos)
    {
        windClone.GetComponent<WindActive>().updateState(Wind.windState.Charging);
        newCyclone.transform.position = CalcWindPrefabPos(blockPos);
        holdDirection = newCyclone.transform.position;
        float actualDistance = Mathf.Clamp(Vector3.Distance(holdDirection, startDirection) / 10, 0.5f, 3.0f);
        newCyclone.transform.localScale = new Vector3(actualDistance, actualDistance, actualDistance);
    }

    void SetNewBreeze()
    {
        newBreeze = windClone.GetComponentInChildren<WindBehavior>().gameObject;
        newBreeze.SetActive(true);
        newBreeze.transform.position = endDirection;
        newBreeze.transform.localEulerAngles = new Vector3(0, 90, 0);
        newBreezeParticle = newBreeze.GetComponentInChildren<ParticleSystem>();
        newBreezeParticle.Play();
    }

    public void SetReleaseWindState()
    {
        if (windState.wind.ActualState == Wind.windState.Charging)
        {
            ToggleArrow(Vector3.zero, Vector3.zero, gameObject, false);
            windState.updateState(Wind.windState.Released);
            endDirection = DestroyNewCyclone();
            SetNewBreeze();      
            releasedWind = WindReleasedState(true);
            SoundEvent(windAudioSource, ventoSolto);
        }
    }

    Vector3 DestroyNewCyclone()
    {
        newCycloneParticle.Stop();
        newCyclone.GetComponent<ParticleSystem>().Stop();
        return newCyclone.transform.position;
    }

    public void CancelWind()
    {
        if (startDirection == endDirection)
        {
            Destroy(windClone);
            SoundEvent(windAudioSource, quebrarLoop);
        }
    }
    private bool WindReleasedState(bool state)
    {
        return state;
    }

    void StopBreezeParticles()
    {
        newBreezeParticle.Stop();
        SoundEvent(windAudioSource, quebrarLoop);
    }

    void ClearWind()
    {
        Destroy(windClone, clearTime);
        Destroy(gameObject, clearTime);
    }

    private Vector3 CalcWindPrefabPos(Vector3 blockPos) => new Vector3(blockPos.x, blockPos.y + windHeight, blockPos.z);

}
