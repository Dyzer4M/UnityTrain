using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TimescaleManager : MonoBehaviour
{
    public float NowTimescale;
    public float gameSpeed = 1;
    public bool working = false;
    public float workingTimescale = 0.5f;
    public void UpdateTimescale()
    {
        if (working)
        {
            Time.timeScale = workingTimescale ;
        }
        else Time.timeScale = gameSpeed;
        NowTimescale = Time.timeScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
