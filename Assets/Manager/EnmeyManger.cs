using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnmeyManger 
{
    // Start is called before the first frame update
    public const string EnemyPath = "C:/Users/daijiahao/ScutMiniGame/Assets/Model/";
    public static GameObject CreatEnemy(string model)
    {
        GameObject obj = Resources.Load<GameObject>(EnemyPath + model);
        GameObject enemy = GameObject.Instantiate(obj);
        return enemy;
    }
}
