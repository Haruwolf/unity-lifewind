using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mathf.Clamp(gameObject.transform.position.y, 25, 60);
        Mathf.Clamp(gameObject.transform.position.z, -50, 0);
        Mathf.Clamp(gameObject.transform.position.x, -8, 20);
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.position += Vector3.forward * Time.deltaTime * 5;
        }

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position += Vector3.left * Time.deltaTime * 5;
        }

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += Vector3.right * Time.deltaTime * 5;
        }

        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position += Vector3.back * Time.deltaTime * 5;
        }
    }
}
