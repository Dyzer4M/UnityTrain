using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct spawn
{
    public Enemy enemy;
    /// <summary>
    //距离上一波怪物生成的时间
    /// </summary>
    public float spawnDelay;
    public Node startNode;
}



public class EnmeyManger : TimeBehavior
{ 
    public int currentSpawn=0;
    public List<spawn> spawnList;


    private void Awake()
    {
        currentSpawn = 0;
    }

    /// <summary>
    //生成当前一波的怪物
    /// </summary>
    public void SpawnCurrent()
    {
        Enemy enemyToSpawn=Instantiate(spawnList[currentSpawn].enemy);
        enemyToSpawn.gameObject.transform.SetParent(this.gameObject.transform);
        enemyToSpawn.gameObject.transform.position=spawnList[currentSpawn].startNode.transform.position;
        enemyToSpawn.currentNode = spawnList[currentSpawn].startNode;
        currentSpawn++;
        if(currentSpawn < spawnList.Count)
            this.StartTimer(new Timer(spawnList[currentSpawn].spawnDelay, SpawnCurrent));
    }



    private void Start()
    {
        if (currentSpawn < spawnList.Count)
            this.StartTimer(new Timer(spawnList[currentSpawn].spawnDelay, SpawnCurrent));   
    }
}
