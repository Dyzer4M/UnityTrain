using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct spawn
{
    public Enemy enemy;
    /// <summary>
    //������һ���������ɵ�ʱ��
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
    //���ɵ�ǰһ���Ĺ���
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
