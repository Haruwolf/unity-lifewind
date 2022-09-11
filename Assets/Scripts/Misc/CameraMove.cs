using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float scrollSpeed;
    public float top;
    public float left;
    public float right;
    public float bottom;

    public float limitX;
    public float limitZ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //limitX += Mathf.Clamp(limitX, -6, 30);
        //limitZ += Mathf.Clamp(limitZ, -8, -40);
        //Vector3 clampPosition = new Vector3(limitX, transform.position.y, limitZ);
        //transform.position = clampPosition;.

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10, 40), transform.position.y, Mathf.Clamp(transform.position.z, -50, -6));
        if (Input.mousePosition.y >= Screen.height * top)
            transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed, Space.World);;

        if (Input.mousePosition.y <= Screen.height * bottom)
            transform.Translate(Vector3.right * Time.deltaTime * scrollSpeed, Space.World);

        if (Input.mousePosition.x >= Screen.width * right)
            transform.Translate(Vector3.forward * Time.deltaTime * scrollSpeed, Space.World);

        if (Input.mousePosition.x <= Screen.width * left)
            transform.Translate(Vector3.back * Time.deltaTime * scrollSpeed, Space.World);
    }
}
