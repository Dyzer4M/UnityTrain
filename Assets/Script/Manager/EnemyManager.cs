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
    //������һ���������ɵ�ʱ��
    public float spawnDelay;
    public int count;
    public Node startNode;

}



public class EnemyManager : TimeBehavior
{
    public static int EnemyAliveCount = 0;
    public int currentSpawn=0;
    public List<spawn> spawnList;
    //��֮����ӳ�
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
    /*���ɵ�ǰһ���Ĺ���
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
            //��֮����ӳ�
            while (EnemyAliveCount > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveDelay);
        }
    }
}
