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
        Released
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

    public void fillCloudHP()
    {
        Ray ray = new Ray(gameObject.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            if (hitInfo.collider.tag == "Water")
            {

                if (hitInfo.collider.gameObject.GetComponent<CloudMaker>().riverCooldown == false)
                {
                    if (cloudHP < cloudMaxHP)
                    {
                        cloudHP += 25.5f * Time.deltaTime;
                        hitInfo.collider.gameObject.GetComponent<CloudMaker>().riverHP -= 10.5f * Time.deltaTime;
                        currentColor -= 25.5f / 255 * Time.deltaTime;
                    }

                    else
                    {
                        cloudHP = Mathf.Clamp(cloudHP, 0, 255);
                        //currentColor = Mathf.Clamp(currentColor, 0, 0);
                    }
                    crystalBar.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                    crystalBar.enabled = true;
                    crystalBar.fillAmount = hitInfo.collider.gameObject.GetComponent<CloudMaker>().riverHP / 100;
                }




                else
                {
                    cloudStateActual = cloudState.Released;


                }
            }
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
        if (gameObjectSound.clip == (AudioClip)Resources.Load("CriandoNuvem") && cloudStateActual == cloudState.Released)
        {
            if (cloudHP > 200 && (AudioClip)Resources.Load("CriandoNuvem") != (AudioClip)Resources.Load("HeavyRain"))
            {
                gameObjectSound.clip = (AudioClip)Resources.Load("HeavyRain");
                gameObjectSound.Play();
                gameObjectSound.loop = true;
                if (GameManager.instance.tut3 == false)
                {
                    TutorialControl.Instance.setTutorial(3, false);
                    GameManager.instance.tut3 = true;
                }
            }

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
            if (cloudHP < 0)
                GetComponent<ParticleSystem>().Stop();

            Mathf.Clamp(GetComponent<Rigidbody>().drag = cloudHP / 25f, 1, 10);
            crystalBar.enabled = false;

        }

        if (cloudHP < 200)
        {
            thunderPrefab.SetActive(false);

        }


        cloudParticle.startColor = new Color(currentColor, currentColor, currentColor);
        //Debug.Log(cloudParticle.startColor.color);




    }
}
