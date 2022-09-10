using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Maciera : MonoBehaviour
{
    public Plant plant;
    public float dryRate = 0.35f;
    public GameObject seed;
    public GameObject sprout;
    public GameObject tree;
    public GameObject macieraPrefab;

    public Image dryBar;
    Image dryBarClone;
    GameObject canvas;

    public bool watering = false;

    private void OnEnable()
    {
        plant = new Plant(
            plantState: Plant.plantStates.Seed,
            iSprout: false,
            iWeed: false,
            canDestroy: false,
            wLevel: 0,
            isIngrained: false,
            spawnSeed: true);

        plant.DryLevel = 120;
        canvas = GameObject.Find("Canvas");

    }

    public void checkGrow(float wLevel)
    {
        plant.WaterLevel = wLevel;
        plant.growStates(plant.WaterLevel);

        seed.SetActive(plant.plantState == Plant.plantStates.Seed);
        sprout.SetActive(plant.plantState == Plant.plantStates.Sprout); 
        tree.SetActive(plant.plantState == Plant.plantStates.Tree);

        if (plant.spawnSeeds == true && plant.plantState == Plant.plantStates.Tree)
        {
            plant.spawnSeeds = false;
            GameObject maciera = Instantiate(macieraPrefab, transform.position, transform.rotation);
            maciera.transform.GetChild(0).gameObject.SetActive(true);
            maciera.transform.GetChild(1).gameObject.SetActive(false);
            maciera.transform.GetChild(2).gameObject.SetActive(false);
            
        }
    }

    public void Update()
    {
        if (plant.plantState == Plant.plantStates.Sprout || plant.plantState == Plant.plantStates.Tree && watering == false)
        {
            plant.DryLevel -= 0.35f * Time.deltaTime;


            if (plant.DryLevel < 60)
            {
                if (dryBarClone == null)
                {
                    dryBarClone = Instantiate(dryBar, canvas.transform);
                    dryBarClone.transform.position = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z));
                    dryBarClone.fillAmount = (plant.DryLevel) / 60;
                }

                else
                {
                    dryBarClone.enabled = true;
                    dryBarClone.transform.position = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z));
                    dryBarClone.fillAmount = (plant.DryLevel) / 60;
                }


            }

            else
            {
                if (dryBarClone != null)
                    dryBarClone.enabled = false;
            }

            Debug.Log(plant.DryLevel);

        }

        if (plant.DryLevel <= 1)
            Destroy(gameObject);

    }

}

