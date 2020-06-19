using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewerController : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame

    public float speed = 7;
    public float mousespeed = 150;

    private float xLeftBorder = -11.0f;
    private float xRightBorder = -1.0f;
    private float zForwardBorder = 5.0f;
    private float zBackBorder = -7.5f;
    private float yUpBorder = 20.0f;
    private float yDownBorder = 10.0f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");//x
        float v = Input.GetAxis("Vertical");//z
        float mouse = Input.GetAxis("Mouse ScrollWheel");

        transform.Translate(new Vector3(h * speed, -mouse * mousespeed, v * speed) * Time.deltaTime, Space.World);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xLeftBorder, xRightBorder),
                                          Mathf.Clamp(transform.position.y, yDownBorder, yUpBorder),
                                          Mathf.Clamp(transform.position.z, zBackBorder, zForwardBorder));
    }
}
