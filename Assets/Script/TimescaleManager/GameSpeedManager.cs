using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedManager : MonoBehaviour
{
    public TimescaleManager timeChanger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void x1()
    {
        timeChanger.gameSpeed = 1;
        timeChanger.UpdateTimescale();
    }
    public void x2()
    {
        timeChanger.gameSpeed = 2;
        timeChanger.UpdateTimescale();
    }
    public void x05()
    {
        timeChanger.gameSpeed = 0.5f;
        timeChanger.UpdateTimescale();
    }
}
