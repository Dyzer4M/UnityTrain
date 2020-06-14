using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TimescaleManager : MonoBehaviour
{
    public ToggleGroup speedGroup;
    public Toggle pauseToggle;
    public float NowTimescale;
    public float gameSpeed = 1;
    public bool working = false;
    public float workingTimescale = 0.5f;
    public bool pause = false;
    public bool isEnd = false;
    public GameObject pauseCanvas;

    public void resetTimescale()
    {
        gameSpeed = 1;
        working = false;
        pause = false;
        isEnd = false;
        UpdateTimescale();
    }
    public void UpdateTimescale()
    {
        if (isEnd || pause) Time.timeScale = 0;
        else if (working)
        {
            Time.timeScale = workingTimescale;
        }
        else Time.timeScale = gameSpeed;
        NowTimescale = Time.timeScale;//只是给开发看的（
    }
    public void SetPause(bool toggleOn)
    {
        pause = toggleOn;
        if (!isEnd) pauseCanvas.SetActive(pause) ;
        UpdateTimescale();
    }

    public void SetEnd()
    {
        isEnd = true ;
        UpdateTimescale();
        for (int i = 0; i < speedGroup.transform.childCount; i++)
        {
            speedGroup.transform.GetChild(i).GetComponent<Toggle>().interactable = false;
            pauseToggle.interactable = false;
        }
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
