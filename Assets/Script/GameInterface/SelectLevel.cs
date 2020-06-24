using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel: MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Index");
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
