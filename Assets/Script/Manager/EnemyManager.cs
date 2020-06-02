using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;




public class EnemyManager : TimeBehavior
{
    public static int EnemyAliveCount = 0;
    public int currentWave=0;
    public List<Wave> waveList;


    //ÿһ����������֮ǰ���ӳ�
    public float waveDelay = 1;

    private void Awake()
    {
      //  currentSpawn = 0;
    }

    /// <summary>
    private void Start()
    {
       // Debug.LogFormat("[��{22}������]");
        SpawnCurrentWave();
    }

    /// </summary>
    ///*���ɵ�ǰһ���Ĺ���
    /// </summary>
    public void SpawnCurrentWave()
    {

        waveList[currentWave].m_callBack += WaveFinish;
        StartTimer(new Timer(waveDelay, waveList[currentWave].StartWave));
    }

    /// <summary>
    /// ÿһ���������ɽ�����Ļص�
    /// </summary>
    public void WaveFinish()
    {
       // Debug.LogFormat("[��{0}������]", currentWave + 1);
        waveList[currentWave].m_callBack -= WaveFinish;
        currentWave++;
        if (currentWave < waveList.Count)
        {
            waveList[currentWave].m_callBack += WaveFinish;
            StartTimer(new Timer(waveDelay, waveList[currentWave].StartWave));
        }
    }

}
