using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct spawn
{
    //public Enemy enemy;
    //prefab
    public GameObject enemyPrefab;
    /// <summary>
    //距离上一波怪物生成的时间
    public float spawnDelay;
    // public int count;
    public Node startNode;

}


public class Wave :TimeBehavior
{
    public static int EnemyAliveCount = 0;
    public int currentSpawn = 0;
    public List<spawn> spawnList;
    //波之间的延迟
    public float waveDelay = 1;

    public Action m_callBack;
    private void Awake()
    {
        currentSpawn = 0;
    }




    public void StartWave()
    {
        StartTimer(new Timer(spawnList[currentSpawn].spawnDelay, SpawnCurrent));
    }
  
    /// </summary>
    public void SpawnCurrent()
    {
        GameObject enemyToSpawn=Instantiate(spawnList[currentSpawn].enemyPrefab);
        GameObject enemyManager = GameObject.Find("EnemyManager");

        enemyToSpawn.transform.SetParent(enemyManager.transform);
        enemyToSpawn.transform.position=spawnList[currentSpawn].startNode.transform.position;

        Enemy enemy = enemyToSpawn.GetComponent<Enemy>();
        enemy.currentDesc = spawnList[currentSpawn].startNode.nextNode;
        EnemyManager.EnemyAliveCount++;
        currentSpawn++;
        if(currentSpawn < spawnList.Count)
        {
            this.StartTimer(new Timer(spawnList[currentSpawn].spawnDelay, SpawnCurrent));
            if (m_callBack != null)
            {
                m_callBack();
            }
        }


    }
    


}
