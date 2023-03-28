using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cloud : MonoBehaviour
{
    public GameObject rainPrefab;

    public float cloudHP;

    [Range(1, 100)]
    public float cloudMaxHP = 100;

    [Range(1, 50)]
    public float decrementHP;

    [Range(0.01f, 1)]
    public float speedSizeRate;

    [Range(0.25f, 1f)]
    public float cloudMinSize;

    [Range(0.75f, 2f)]
    public float cloudMaxSize;

    [Range(0, 50)]
    public float cloudSizeRate;

    public AudioSource audio;
    public delegate void cloudSoundDelegateEvent(AudioSource audio, string soundName);
    public static event cloudSoundDelegateEvent SoundEvent;

    public const string criandoNuvem = "CriandoNuvem"; //const = static, ambos podem ser acessados globalmente
    public const string chuvaCaindo = "ChuvaFraca";


    public enum cloudState
    {
        Holding,
        Released,
        Destroyed,
    }

    public cloudState cloudStateActual;
    private void OnEnable()
    {
        TargetSelector.stateObserver += MakeItRain;
    }

    public void OnDisable()
    {
        TargetSelector.stateObserver -= MakeItRain;
    }

    public void MakeItRain()
    {
        rainPrefab.SetActive(true);
        
        if (cloudHP > 0)
        {
            cloudHP -= decrementHP * Time.deltaTime;
            ShrinkCloud();
            //Invoke(nameof(MakeItRain), speedSizeRate);
            SoundEvent(audio, chuvaCaindo);
        }

        else
            Destroy(gameObject);
    }

    public void FillCloud(GameObject actualBlock)
    {
        if (cloudHP <= cloudMaxHP)
            GrowCloud();

        else
            cloudHP = Mathf.Clamp(cloudHP, 0, cloudMaxHP);
    }

    private void GrowCloud()
    {
        cloudHP += cloudSizeRate * Time.deltaTime;
        Vector3 cloudNewScale = ClampVectors(transform.localScale);
        cloudNewScale += new Vector3 (cloudSizeRate, cloudSizeRate, cloudSizeRate) * Time.deltaTime;
        transform.localScale = cloudNewScale;
        SoundEvent(audio, criandoNuvem);
    }

    private Vector3 ClampVectors(Vector3 cloudScale)
    {
        Vector3 newScale = cloudScale;
        newScale.x = Mathf.Clamp(newScale.x, cloudMinSize, cloudMaxSize);
        newScale.y = Mathf.Clamp(newScale.y, cloudMinSize, cloudMaxSize);
        newScale.z = Mathf.Clamp(newScale.z, cloudMinSize, cloudMaxSize);
        return newScale;   
    }

    private void ShrinkCloud()
    {
        cloudHP -= cloudSizeRate * Time.deltaTime;
        Vector3 cloudNewScale = ClampVectors(transform.localScale);
        cloudNewScale -= new Vector3 (cloudSizeRate, cloudSizeRate, cloudSizeRate) * Time.deltaTime;;
        transform.localScale = cloudNewScale;
    }

    //Fazer a �gua regar as plantas
    //Tirar erva daninha
    //Colocar outros tipos de plantas
}
