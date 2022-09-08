using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        cloudParticle.startColor = new Color(1,1,1,1);
        currentColor = 1;

        thunderPrefab = transform.GetChild(1).gameObject;
        thunderPrefab.SetActive(false);

        rainPrefab = transform.GetChild(0).gameObject;
        rainPrefab.SetActive(false);

    }

    public void fillCloudHP()
    {
        if (cloudHP < cloudMaxHP)
        {
            cloudHP += 25.5f * Time.deltaTime;
            currentColor -= 25.5f / 255 * Time.deltaTime;
        }

        else
        {
            cloudHP = Mathf.Clamp(cloudHP, 0, 255);
            //currentColor = Mathf.Clamp(currentColor, 0, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (cloudStateActual == cloudState.Released)
        {
            rainPrefab.SetActive(true);
            if (cloudHP > 200)
                thunderPrefab.SetActive(true);

            cloudHP -= decrementHP * Time.deltaTime;
            currentColor += decrementHP / 255 * Time.deltaTime;
            if (cloudHP < 0)
                GetComponent<ParticleSystem>().Stop();

            
        }

        if (cloudHP < 200)
        {
            thunderPrefab.SetActive(false);

        }


        cloudParticle.startColor = new Color(currentColor, currentColor, currentColor);
        //Debug.Log(cloudParticle.startColor.color);




    }
}
