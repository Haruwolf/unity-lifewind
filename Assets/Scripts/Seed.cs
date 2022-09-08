using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public Maciera plantGameObject;

    private void OnEnable()
    {
        plantGameObject = gameObject.transform.parent.GetComponent<Maciera>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Wind")
        {
            if (plantGameObject.plant.isIngrained == false && other.gameObject.GetComponentInParent<WindActive>().wind.ActualState == Wind.windState.Released)
            {
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

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wind" && other.gameObject.GetComponentInParent<WindActive>().wind.ActualState == Wind.windState.None)
        {
            
            plantGameObject.transform.SetParent(null);
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
                            plantGameObject.plant.isIngrained = true;
                            blockLanded.GetComponent<BlockState>().occupiedBlock = true;
                            blockLanded.GetComponent<BlockState>().AroundObjects();
                            plantGameObject.transform.position = new Vector3(blockLanded.gameObject.transform.position.x, blockLanded.gameObject.transform.position.y + 1, blockLanded.gameObject.transform.position.z);
                        }
                        break;
                    case "Water":
                        Debug.Log(blockLanded.gameObject.tag);
                        Destroy(plantGameObject.gameObject);
                        break;
                    case "OoB":
                        Debug.Log(blockLanded.gameObject.tag);
                        Destroy(plantGameObject.gameObject);
                        break;

                    default:
                        break;
                }

            }

        }
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (plantGameObject.plant.isIngrained)
    //    {

    //    }
    //}
}
