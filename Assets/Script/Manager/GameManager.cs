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
    public Text message;

    public static GameManager Instance;
    public Animator GameAnimation;
    private void Awake()
    {
        Instance = this;
        timeChanger.resetTimescale();
    }
    // Start is called before the first frame update
    public void lose()
    {
        message.text = "Lose";
        GameAnimation.SetTrigger("lose");
        timeChanger.SetEnd();
    }
    public void win()
    {
        message.text="Win";
        GameAnimation.SetTrigger("win");
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
