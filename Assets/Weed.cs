using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weed : MonoBehaviour
{
    public Plant plant;

    public GameObject weed;

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
    }

    private void OnDisable()
    {
        GameManager.instance.weedsOnScreen -= 1;
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wind")
            if (other.gameObject.GetComponentInParent<WindActive>().windSpeed > 2 && other.gameObject.GetComponentInParent<WindActive>().wind.ActualState == Wind.windState.Released)
            {
                GameManager.instance.fillBar += 0.25f;
                Destroy(gameObject);
                Ray ray = new Ray(transform.position, Vector3.down);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
                {
                    if (hitInfo.collider.gameObject.tag == "Grass")
                    {
                        hitInfo.collider.gameObject.GetComponent<BlockState>().waterLevel = 0;
                        hitInfo.collider.gameObject.GetComponent<BlockState>().occupiedBlock = false;
                        hitInfo.collider.gameObject.GetComponent<BlockState>().canCreateWeeds = true;
                    }
                }
            }



    }
}

