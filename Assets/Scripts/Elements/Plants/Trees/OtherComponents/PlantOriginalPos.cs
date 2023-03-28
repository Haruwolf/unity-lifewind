using UnityEngine;

[RequireComponent(typeof(PlantController))]
public class PlantOriginalPos : MonoBehaviour
{
    private Vector3 originalPos { get; set; }

    private void OnEnable()
    {
        originalPos = gameObject.transform.position;
    }

    public Vector3 GetOriginalPos()
    {
        return originalPos;
    }
}
