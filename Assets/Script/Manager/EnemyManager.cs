using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;




public class EnemyManager : TimeBehavior
{
    public static int EnemyAliveCount = 0;
    public int currentWave=0;
    public List<Wave> waveList;


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
        StartTimer(new Timer(waveDelay, waveList[currentWave].StartWave));
    }

    /// <summary>
    /// 每一波怪物生成结束后的回调
    /// </summary>
    public void WaveFinish()
    {
       // Debug.LogFormat("[第{0}波结束]", currentWave + 1);
        waveList[currentWave].m_callBack -= WaveFinish;
        currentWave++;
        if (currentWave < waveList.Count)
        {
            waveList[currentWave].m_callBack += WaveFinish;
            StartTimer(new Timer(waveDelay, waveList[currentWave].StartWave));
        }
    }

}
