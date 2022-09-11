using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weed : MonoBehaviour
{
    public Plant plant;

    public GameObject weed;
    public GameObject blockLanded;

    private void OnEnable()
    {
        plant = new Plant(
            plantState: Plant.plantStates.NotSet,
            iSprout: false,
            iWeed: true,
            canDestroy: true,
            wLevel: 0,
            isIngrained: true,
            spawnSeed: false);

        GameManager.instance.weedsOnScreen += 1;

        if (GameManager.instance.tut6 == false)
        {
            TutorialControl.Instance.setTutorial(6, true);
            GameManager.instance.tut6 = true;
        }
    }

    private void OnDisable()
    {
        GameManager.instance.weedsOnScreen -= 1;
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wind")
            if (other.gameObject.GetComponentInParent<WindActive>().windSpeed > 3 && other.gameObject.GetComponentInParent<WindActive>().wind.ActualState == Wind.windState.Released)
            {
                
                soundEffect();
                GameManager.instance.fillBar += 0.25f;
                blockLanded.GetComponent<BlockState>().occupiedBlock = false;
                blockLanded.GetComponent<BlockState>().canCreateWeeds = true;
                blockLanded.GetComponent<BlockState>().waterLevel = 0;
                ShakeEffect.instance.shakeScreen();
                Destroy(gameObject, 0.25f);
            }



    }

    

    void soundEffect()
    {
        AudioSource gameObjectSound = gameObject.AddComponent<AudioSource>();
        gameObjectSound.clip = (AudioClip)Resources.Load("Destruicao");
        gameObjectSound.Play();
        gameObjectSound.loop = false;

    }
}

