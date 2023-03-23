using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Wind : MonoBehaviour
{
    public GameObject windParentPrefab;

    //[HideInInspector]
    public Vector3 startDirection, endDirection, holdDirection;

    [SerializeField]
    [Range(0f, 10f)]
    float windHeight = 7.5f;

    GameObject newCyclone;
    GameObject newBreeze;
    GameObject windClone;

    public ParticleSystem cycloneParticle;
    public ParticleSystem breezeParticle;
    public AudioSource windAudioSource;

    WindStatus windState;
    ParticleSystem newCycloneParticle;
    ParticleSystem newBreezeParticle;

    bool releasedWind;
    float windTimer;

    public float interpolationTime = 5;
    public float clearTime = 4;

    public float windOffsetHeight = 0.25f;
    public UnityEvent OnWindFinished;

    public delegate void arrowDelegateEvent(Vector3 startDir, Vector3 holdDir, GameObject windManager, bool toggle);
    public static event arrowDelegateEvent ToggleArrow;

    public delegate void soundDelegateEvent(AudioSource audio, string soundName);
    public static event soundDelegateEvent SoundEvent;

    public const string ventoCarregando = "ventoCarregando"; //const = static, ambos podem ser acessados globalmente
    public const string ventoSolto = "ventoSolto";
    public const string quebrarLoop = "quebrarLoop";



    private void Update()
    {
        CheckWindChargingState();
        MoveBreeze();
        CheckIfWindFinished();
    }

    private void CheckWindChargingState()
    {
        if (Time.deltaTime == 0 && windClone != null && windClone.GetComponent<WindStatus>().wind.ActualState == WindObject.windState.Charging)
        {
            SetReleaseWindState();
        }
    }

    private void MoveBreeze()
    {
        if (releasedWind && newBreeze != null)
        {
            windTimer += Time.deltaTime;
            newBreeze.transform.position = Vector3.Lerp(endDirection, startDirection, windTimer / interpolationTime);
        }
    }

    private void CheckIfWindFinished()
    {
        if (releasedWind && newBreeze != null && windTimer >= interpolationTime && windClone != null)
        {
            try
            {
                //Descobrir que tipo de evento está atrelado a isso.
                releasedWind = WindReleasedState(false);
                StopBreezeParticles();
                OnWindFinished?.Invoke(); // Obtém uma matriz de delegados dos métodos inscritos no evento
                OnWindFinished.RemoveAllListeners();
                ClearWind();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }


    public void CreateWindPrefab(GameObject actualBlock)
    {
        Vector3 windInitPos = CalcWindPrefabPos(actualBlock.transform.position);
        windClone = Instantiate(windParentPrefab, gameObject.transform);
        WindCloneConfigs(windClone);
        newCyclone = CreateNewCyclone(windInitPos, windClone);
        windState = windClone.GetComponent<WindStatus>();
        windState.updateState(WindObject.windState.Setted);
        startDirection = newCyclone.transform.position;
    }

    void WindCloneConfigs(GameObject windClone)
    {
        windClone.transform.SetParent(gameObject.transform);
        windClone.name = "Wind";
    }

    public Wind GetWind()
    {
        return gameObject.GetComponent<Wind>();
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
        if (windState.wind.ActualState == WindObject.windState.Setted || windState.wind.ActualState == WindObject.windState.Charging)
        {
            ChargeCyclone(actualBlock.transform.position);
            ToggleArrow(startDirection, holdDirection, gameObject, true);
            SoundEvent(windAudioSource, ventoCarregando);
        }

    }

    void ChargeCyclone(Vector3 blockPos)
    {
        windClone.GetComponent<WindStatus>().updateState(WindObject.windState.Charging);
        newCyclone.transform.position = CalcWindPrefabPos(blockPos);
        holdDirection = newCyclone.transform.position;
        //float actualDistance = Mathf.Clamp(Vector3.Distance(holdDirection, startDirection) / 10, 1f, 2f);
        //newCyclone.transform.localScale = new Vector3(actualDistance, actualDistance, actualDistance);
    }

    void SetNewBreeze()
    {
        newBreeze = windClone.GetComponentInChildren<Breeze>().gameObject;
        newBreeze.SetActive(true);
        newBreeze.transform.position = endDirection;
        //newBreeze.transform.localEulerAngles = new Vector3(0, 90, 0);
        newBreeze.transform.LookAt(startDirection);
        newBreezeParticle = newBreeze.GetComponentInChildren<ParticleSystem>();
        newBreezeParticle.Play();
    }

    public void SetReleaseWindState()
    {
        if (windState.wind.ActualState == WindObject.windState.Charging)
        {
            ToggleArrow(Vector3.zero, Vector3.zero, gameObject, false);
            windState.updateState(WindObject.windState.Released);
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

    private Vector3 CalcWindPrefabPos(Vector3 blockPos) => new Vector3(blockPos.x, blockPos.y + windOffsetHeight, blockPos.z);

}
