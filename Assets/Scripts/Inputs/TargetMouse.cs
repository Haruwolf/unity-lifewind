using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMouse : MonoBehaviour
{

    private Material[] materials;
    private Color firstOriginalColor;
    private Color secondOriginalColor;
    // Start is called before the first frame update
    void Start()
    {
        materials = GetComponent<Renderer>().materials;
        firstOriginalColor = materials[0].color;
        secondOriginalColor = materials[1].color;
    }

    private void OnMouseEnter()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            if (hitInfo.collider.tag == "Grass" || hitInfo.collider.tag == "Water")
            {
                materials[0].color = Color.yellow;
                materials[1].color = Color.yellow;
            }
        }

    }

    private void OnMouseExit()
    {
        materials[0].color = firstOriginalColor;
        materials[1].color = secondOriginalColor;
    }
}
