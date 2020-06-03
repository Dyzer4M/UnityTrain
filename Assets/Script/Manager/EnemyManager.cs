using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;




[System.Serializable]
public class EnemyManager : TimeBehavior
{
  
    public int currentWave=0;
    public List<Wave> waveList;
    public  int EnemyAliveCount = 0;

    public static  bool spawnFinish = false;

    //每一波怪物生成之前的延迟
    public float waveDelay = 1;



    private void Awake()
    {
      //  currentSpawn = 0;
    }

    /// <summary>
    private void Start()
    {
       // Debug.LogFormat("[第{22}波结束]");
        SpawnCurrentWave();
    }

    /// </summary>
    ///*生成当前一波的怪物
    /// </summary>
    public void SpawnCurrentWave()
    {

        waveList[currentWave].m_callBack += WaveFinish;
        waveList[currentWave].m_aliveCallBack += aliveCallback;
        waveList[currentWave].m_deadCallBack += destroyCallback;
        StartTimer(new Timer(waveDelay, waveList[currentWave].StartWave));
    }

    /// <summary>
    /// 每一波怪物生成结束后的回调
    /// </summary>
    public void WaveFinish()
    {
       // Debug.LogFormat("[第{0}波结束]", currentWave + 1);
        //waveList[currentWave].m_deadCallBack -= deadCallback;
        waveList[currentWave].m_callBack -= WaveFinish;
        currentWave++;
        if (currentWave < waveList.Count)
        {
            waveList[currentWave].m_callBack += WaveFinish;
            waveList[currentWave].m_aliveCallBack += aliveCallback;
            waveList[currentWave].m_deadCallBack += destroyCallback;
            StartTimer(new Timer(waveDelay, waveList[currentWave].StartWave));
        }
        else
            spawnFinish = true;
    }


    public void aliveCallback(Enemy enemy)
    {

        EnemyAliveCount++;
    }


    /// <summary>
    /// 怪物死亡回调
    /// </summary>
    public void destroyCallback(Enemy enemy)
    {
        enemy.deadCallback = null;
        enemy.aliveCallback = null;
        EnemyAliveCount--;
        Debug.Log("enemy die");
        if (enemy.isArrive)
        {
            //游戏失败
            GameOver();
            return;
        }
        else if( spawnFinish&&EnemyAliveCount == 0)
        {
            GameWin();
            return;
        }
    }
    /// <summary>
    /// 游戏失败逻辑
    /// </summary>
    public void GameOver()
    {
        Debug.Log("游戏失败");
    }
    public void GameWin()
    {
        Debug.Log("游戏胜利");
    }

}
