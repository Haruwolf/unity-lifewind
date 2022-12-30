using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cloud : MonoBehaviour
{
    public GameObject rainPrefab;

    public float cloudHP;

    [Range(1, 255)]
    public float cloudMaxHP = 255;

    [Range(1, 50)]
    public float decrementHP;

    [Range(0.01f, 1)]
    public float speedSizeRate;

    [Range(1, 5)]
    public int cloudMinSize;

    [Range(1, 5)]
    public int cloudMaxSize;

    [Range(0, 50)]
    public int cloudSizeRate;

    [Range(50, 100)]
    public int cloudScaleOffset = 65;


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
            Invoke(nameof(MakeItRain), speedSizeRate);
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
        float newSize = Mathf.Clamp(cloudHP / cloudScaleOffset, cloudMinSize, cloudMaxSize);
        transform.localScale = new Vector3(newSize, newSize, newSize);
        SoundEvent(audio, criandoNuvem);
    }

    private void ShrinkCloud()
    {
        cloudHP -= cloudSizeRate * Time.deltaTime;
        float newSize = Mathf.Clamp(cloudHP / cloudScaleOffset, cloudMinSize, cloudMaxSize);
        transform.localScale = new Vector3(newSize, newSize, newSize);
    }

    //Fazer a água regar as plantas
    //Tirar erva daninha
    //Colocar outros tipos de plantas
}
