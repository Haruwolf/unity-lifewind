using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : MonoBehaviour
{
    private Renderer rendererGameObject;
    private Color originalColor;

    public bool canCreateWeeds;

    public float waterLevel;
    // Start is called before the first frame update
    void Start()
    {
        rendererGameObject = GetComponent<Renderer>();
        originalColor = rendererGameObject.material.color;
        canCreateWeeds = true;
    }

    public bool occupiedBlock;
    List<GameObject> blocksAround = new List<GameObject>();

    public GameObject weedGameObject;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Plant")
        {
            //occupiedBlock = true;
            //gameObject.tag = "OoB";
            rendererGameObject.material.color = Color.red;
        }
    }

    private void OnTriggerStay(Collider other)
    {
       if (other.gameObject.tag == "Cloud" && canCreateWeeds == true)
        {
            waterLevel += 1 * Time.deltaTime;
        }

       if (waterLevel > 10 && canCreateWeeds == true && occupiedBlock == false)
        {
            canCreateWeeds = false;
            //gameObject.tag = "OoB";
            Instantiate(weedGameObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z), weedGameObject.transform.rotation);
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Plant")
        {
            //occupiedBlock = false;
            //gameObject.tag = "Grass";
            rendererGameObject.material.color = originalColor;
        }

    }

    //public void AroundObjects()
    //{
    //    Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 5);
    //    foreach (Collider collider in hitColliders)
    //    {
    //        if (collider.gameObject.tag == "Grass")
    //        {
    //            if (collider.gameObject != gameObject)
    //            {
    //                canCreateWeeds = true;
    //                blocksAround.Add(collider.gameObject);
    //                Debug.Log(collider.gameObject.name);
    //                createWeeds(blocksAround);
    //                collider.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
    //            }
    //        }
            
    //    }


    //}

    //void createWeeds(List<GameObject> blocksAround)
    //{
    //    int indexChoosed = Random.Range(0, blocksAround.Count);
    //    GameObject weedBlock = blocksAround[indexChoosed].gameObject;
    //    Instantiate(weedGameObject, new Vector3(weedBlock.transform.position.x, 1, weedBlock.transform.position.z), weedBlock.transform.rotation);

    //}
}
