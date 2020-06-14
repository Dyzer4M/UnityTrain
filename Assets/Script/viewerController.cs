using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewerController : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame

    public float speed = 4;
    public float mousespeed = 60;
    void Update()
    {
        float h = Input.GetAxis("Horizontal");//x
        float v = Input.GetAxis("Vertical");//z
        float mouse = Input.GetAxis("Mouse ScrollWheel");

        transform.Translate(new Vector3(h*speed, mouse*mousespeed, v*speed) * Time.deltaTime, Space.World);
    }
}
