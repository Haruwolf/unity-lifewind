using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMouse : MonoBehaviour
{
    private Renderer rendererGameObject;
    private Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        rendererGameObject = GetComponent<Renderer>();
        originalColor = rendererGameObject.material.color;
    }

    private void OnMouseEnter()
    {
        rendererGameObject.material.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        rendererGameObject.material.color = originalColor;
    }
}
