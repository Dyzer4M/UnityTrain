using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TimescaleManager timeChanger;
    //界面
    public GameObject GameCanves;
    public Text message;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    public void lose()
    {
        message.text = "Lose";
        GameCanves.SetActive(true);
        timeChanger.SetEnd();
    }
    public void win()
    {
        message.text="Win";
        GameCanves.SetActive(true);
        timeChanger.SetEnd();
    }
    public void ReplayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
