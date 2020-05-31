using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public struct spawn
{
    public Enemy enemy;
    //prefab
    public GameObject enemyPrefab;
    /// <summary>
    //距离上一波怪物生成的时间
    public float spawnDelay;
    public int count;
    public Node startNode;

}



public class EnemyManager : TimeBehavior
{
    public static int EnemyAliveCount = 0;
    public int currentSpawn=0;
    public List<spawn> spawnList;
    //波之间的延迟
    public float waveDelay = 1;

    private void Awake()
    {
        currentSpawn = 0;
    }

    /// <summary>
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    /*生成当前一波的怪物
    /// </summary>
    public void SpawnCurrent()
    {
        Enemy enemyToSpawn=Instantiate(spawnList[currentSpawn].enemy);
        enemyToSpawn.gameObject.transform.SetParent(this.gameObject.transform);
        enemyToSpawn.gameObject.transform.position=spawnList[currentSpawn].startNode.transform.position;
        enemyToSpawn.currentDesc = spawnList[currentSpawn].startNode.nextNode;
        currentSpawn++;
        if(currentSpawn < spawnList.Count)
            this.StartTimer(new Timer(spawnList[currentSpawn].spawnDelay, SpawnCurrent));
    }
    */

    IEnumerator SpawnEnemy()
    {
        for(; currentSpawn<spawnList.Count;currentSpawn++)
        {
            for(int i = 0; i < spawnList[currentSpawn].count; i++)
            {
                //GameObject t = GameObject.Instantiate(spawnList[currentSpawn].enemyPrefab, spawnList[currentSpawn].startNode.transform.position,Quaternion.identity);
                Enemy enemyToSpawn = Instantiate(spawnList[currentSpawn].enemy);
                enemyToSpawn.gameObject.transform.SetParent(this.gameObject.transform);
                enemyToSpawn.gameObject.transform.position = spawnList[currentSpawn].startNode.transform.position;
                enemyToSpawn.currentDesc = spawnList[currentSpawn].startNode.nextNode;
                EnemyAliveCount++;
                if(i!=spawnList[currentSpawn].count-1)
                    yield return new WaitForSeconds(spawnList[currentSpawn].spawnDelay);
            }
            //波之间的延迟
            while (EnemyAliveCount > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveDelay);
        }
    }
}
