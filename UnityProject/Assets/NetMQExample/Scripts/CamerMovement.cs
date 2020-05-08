using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamerMovement : MonoBehaviour
{
    public Camera camera;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw   = 0.0f;
    private float pitch = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {


            camera.transform.position = camera.transform.position + new Vector3(0, 0.1f, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            camera.transform.position = camera.transform.position + new Vector3(-0.1f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            camera.transform.position = camera.transform.position + new Vector3(0, -0.1f, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            camera.transform.position = camera.transform.position + new Vector3(0.1f, 0, 0);
        }


        yaw += speedH * Input.GetAxis("Mouse X");

        pitch -= speedV * Input.GetAxis("Mouse Y");



        if (Input.GetKey(KeyCode.Q))
        {
            camera.transform.Rotate(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            camera.transform.Rotate(0, -1, 0);
        }

    }
}
