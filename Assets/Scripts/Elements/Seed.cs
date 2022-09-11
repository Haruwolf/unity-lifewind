using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    Maciera plantGameObject;
    Vector3 originalPos;
    ParticleSystem.MainModule particle;
    

    private void OnEnable()
    {
        plantGameObject = gameObject.transform.parent.GetComponent<Maciera>();
        originalPos = plantGameObject.transform.localPosition;
        AudioControl.instance.audioSource.clip = (AudioClip)Resources.Load("PlantPop");
        AudioControl.instance.audioSource.Play();
        AudioControl.instance.audioSource.loop = false;
        particle = gameObject.GetComponent<ParticleSystem>().main;
        particle.startColor = Color.yellow;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Wind")
        {
            if (plantGameObject.plant.isIngrained == false && other.gameObject.GetComponentInParent<WindActive>().wind.ActualState == Wind.windState.Released)
            {
                Debug.Log("Entered");
                plantGameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
            }
        }

        if (other.gameObject.tag == "Cloud")
        {
            if (plantGameObject.plant.isIngrained == true)
            {
                
                plantGameObject.plant.WaterLevel += 0.5f * Time.deltaTime;
                plantGameObject.plant.growStates(plantGameObject.plant.WaterLevel);
                plantGameObject.checkGrow(plantGameObject.plant.WaterLevel);

            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cloud")
        {
            if (plantGameObject.plant.isIngrained == true && GameManager.instance.tut4 == false)
            {
                GameManager.instance.tut4 = true;
                TutorialControl.Instance.setTutorial(4, false);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Wind")
        {
            Debug.Log("Got Out");
            setPlantOnCube(plantGameObject.transform.position);

        }

    }

    public void setPlantOnCube(Vector3 plantPos)
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            if (hitInfo.collider != null)
            {
                GameObject blockLanded = hitInfo.collider.gameObject;
                switch (blockLanded.gameObject.tag)
                {
                    case "Grass":
                        if (plantGameObject.plant.isIngrained == false)
                        {
                            if(blockLanded.GetComponent<BlockState>().occupiedBlock == false)
                            {
                                particle.startColor = Color.green;
                                plantGameObject.plant.isIngrained = true;
                                blockLanded.GetComponent<BlockState>().occupiedBlock = true;
                                plantGameObject.blockLanded = blockLanded;
                                if (GameManager.instance.tut2 == false)
                                {
                                    TutorialControl.Instance.setTutorial(2, false);
                                    GameManager.instance.tut2 = true;
                                }
                                
                                //blockLanded.GetComponent<BlockState>().AroundObjects();
                                plantGameObject.transform.position = new Vector3(blockLanded.gameObject.transform.position.x, blockLanded.gameObject.transform.position.y + 1, blockLanded.gameObject.transform.position.z);
                                //blockLanded.gameObject.tag = "OoB";
                            }

                            else
                            {
                                plantGameObject.transform.position = originalPos;
                                plantGameObject.plant.isIngrained = false;
                            }


                        }
                        break;
                    case "Water":
                        plantGameObject.transform.position = originalPos;
                        plantGameObject.plant.isIngrained = false;
                        break;
                    case "OoB":
                        plantGameObject.transform.position = originalPos;
                        plantGameObject.plant.isIngrained = false;
                        break;

                    default:
                        break;
                }

            }

        }

        if (hitInfo.collider == null)
        {
            plantGameObject.transform.position = originalPos;
            plantGameObject.plant.isIngrained = false;
        }
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (plantGameObject.plant.isIngrained)
    //    {

    //    }
    //}
}
