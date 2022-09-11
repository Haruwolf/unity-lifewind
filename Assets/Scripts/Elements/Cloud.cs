using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cloud : MonoBehaviour
{
    public float cloudHP;
    public float cloudMaxHP = 255;
    public float decrementHP = 0.25f;

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

    public void fillCloudHP(GameObject actualBlock)
    {

        if (actualBlock.GetComponent<CloudMaker>().riverCooldown == false)
        {
            if (cloudHP <= cloudMaxHP)
            {
                cloudHP += 20.5f * Time.deltaTime;
                actualBlock.GetComponent<CloudMaker>().riverHP -= 10.5f * Time.deltaTime;
                currentColor -= 25.5f / 255 * Time.deltaTime;
            }

            else
            {
                cloudHP = Mathf.Clamp(cloudHP, 0, 255);
                //currentColor = Mathf.Clamp(currentColor, 0, 0);
            }
            crystalBar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 6, gameObject.transform.position.z));
            crystalBar.enabled = true;
            crystalBar.transform.localScale = new Vector3(4, 4, 4);
            crystalBar.fillAmount = actualBlock.GetComponent<CloudMaker>().riverHP / 100;
        }




        else
        {
            cloudStateActual = cloudState.Released;


        }
    }


    void soundEffectRain()
    {

    }

    void soundEffectThunder()
    {
        AudioSource gameObjectSound = gameObject.AddComponent<AudioSource>();
        gameObjectSound.clip = (AudioClip)Resources.Load("Destruicao");
        gameObjectSound.Play();
        gameObjectSound.loop = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime == 0)
            cloudStateActual = cloudState.Released;

        if (gameObjectSound.clip == (AudioClip)Resources.Load("CriandoNuvem") && cloudStateActual == cloudState.Released)
        {
            //if (cloudHP > 200 && AudioControl.instance.audioSource.clip != (AudioClip)Resources.Load("HeavyRain"))
            //{
            //    gameObjectSound.clip = (AudioClip)Resources.Load("HeavyRain");
            //    gameObjectSound.Play();
            //    gameObjectSound.loop = true;
            //    if (GameManager.instance.tut3 == false)
            //    {
            //        TutorialControl.Instance.setTutorial(3, false);
            //        GameManager.instance.tut3 = true;
            //    }
            //}

            if (cloudHP < 200 && AudioControl.instance.audioSource.clip != (AudioClip)Resources.Load("ChuvaFraca"))
            {
                gameObjectSound.clip = (AudioClip)Resources.Load("ChuvaFraca");
                gameObjectSound.Play();
                gameObjectSound.loop = true;
                if (GameManager.instance.tut3 == false)
                {
                    TutorialControl.Instance.setTutorial(3, false);
                    GameManager.instance.tut3 = true;
                }

            }
        }
        if (cloudStateActual == cloudState.Released)
        {

            rainPrefab.SetActive(true);
            if (cloudHP > 200)
                thunderPrefab.SetActive(true);

            cloudHP -= decrementHP * Time.deltaTime;
            currentColor += decrementHP / 255 * Time.deltaTime;
            if (cloudHP < 5)
            {
                cloudStateActual = cloudState.Destroyed;
                GetComponent<ParticleSystem>().Stop();
                TargetSelector.instance.cloudCount--;
            }

            //Mathf.Clamp(GetComponent<Rigidbody>().drag = cloudHP / 25f, 7f, 7f);

            crystalBar.enabled = false;

        }

        if (cloudStateActual != cloudState.Holding)
            crystalBar.enabled = false;

        if (cloudStateActual == cloudState.Destroyed)
            Destroy(gameObject);

        if (cloudHP < 200)
        {
            thunderPrefab.SetActive(false);

        }


        cloudParticle.startColor = new Color(currentColor, currentColor, currentColor);
        float newSize = Mathf.Clamp(cloudHP / 65, 1, 4f);
        transform.localScale = new Vector3(newSize, newSize, newSize);
        //Debug.Log(cloudParticle.startColor.color);




    }
}
