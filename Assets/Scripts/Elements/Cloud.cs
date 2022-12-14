using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cloud : MonoBehaviour
{
    public float cloudHP;

    [Range(1,255)]
    public float cloudMaxHP = 255;
    [Range(0.1f, 1)]
    public float decrementHP = 0.25f;

    [Range(1, 5)]
    public int cloudMinSize;

    [Range(1,5)]
    public int cloudMaxSize;

    [Range(0, 50)]
    public int cloudIncreaseRate;

    [Range(50, 100)]
    public int cloudOffsetHP = 65;

    [Range(50, 100)]
    public float colorChangeRate= 25.5f;

    GameObject thunderPrefab;
    GameObject rainPrefab;

    Rigidbody cloudRb;
    Color cloudColor;
    ParticleSystem.MainModule cloudParticle;


    float currentColor;

    public Image crystalBar;
    public GameObject canvas;

    AudioSource gameObjectSound;

    public enum cloudState
    {
        Holding,
        Released,
        Destroyed,
    }

    public cloudState cloudStateActual;
    private void OnEnable()
    {
        cloudHP = 1;
        cloudRb = GetComponent<Rigidbody>();
        cloudParticle = GetComponent<ParticleSystem>().main;
        cloudParticle.startColor = new Color(1, 1, 1, 1);
        currentColor = 1;

        thunderPrefab = transform.GetChild(1).gameObject;
        thunderPrefab.SetActive(false);

        rainPrefab = transform.GetChild(0).gameObject;
        rainPrefab.SetActive(false);

        canvas = GameObject.Find("Canvas");
        crystalBar = Instantiate(crystalBar, canvas.transform);

        gameObjectSound = gameObject.AddComponent<AudioSource>();
        gameObjectSound.clip = (AudioClip)Resources.Load("CriandoNuvem");
        gameObjectSound.Play();
        gameObjectSound.loop = true;
        gameObjectSound.volume = 0.25f;


    }

    public void FillCloud(GameObject actualBlock)
    {
        if (cloudHP <= cloudMaxHP)
        {
            GrowCloud();
            TintCloud();
        }

        else
            cloudHP = Mathf.Clamp(cloudHP, 0, cloudMaxHP);


    }

    private void GrowCloud()
    {
        cloudHP += cloudIncreaseRate * Time.deltaTime;
        float newSize = Mathf.Clamp(cloudHP / cloudOffsetHP, cloudMinSize, cloudMaxSize);
        transform.localScale = new Vector3(newSize, newSize, newSize);


    }

    private void TintCloud()
    {
        currentColor -= colorChangeRate / cloudMaxHP * Time.deltaTime;
    }

    //colocar pra quando a nuvem estiver solta, diminuir o HP dela e consequentemente o tamanho e a cor.
    //fazer chover
    void Update()
    {

        cloudParticle.startColor = new Color(currentColor, currentColor, currentColor);


    }
}
