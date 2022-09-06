using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : MonoBehaviour
{
    private Renderer rendererGameObject;
    private Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        rendererGameObject = GetComponent<Renderer>();
        originalColor = rendererGameObject.material.color;
    }

    public bool occupiedBlock;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Plant")
        {
            //occupiedBlock = true;
            //gameObject.tag = "OoB";
            rendererGameObject.material.color = Color.red;
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
}
