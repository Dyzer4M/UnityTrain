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
        waveList[currentWave].m_aliveCallBack += aliveCallback;
        waveList[currentWave].m_deadCallBack += destroyCallback;
        StartTimer(new Timer(waveDelay, waveList[currentWave].StartWave));
    }

    /// <summary>
    /// ÿһ���������ɽ�����Ļص�
    /// </summary>
    public void WaveFinish()
    {
       // Debug.LogFormat("[��{0}������]", currentWave + 1);
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
    /// ���������ص�
    /// </summary>
    public void destroyCallback(Enemy enemy)
    {
        enemy.deadCallback = null;
        enemy.aliveCallback = null;
        EnemyAliveCount--;
        Debug.Log("enemy die");
        if (enemy.isArrive)
        {
            //��Ϸʧ��
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
    /// ��Ϸʧ���߼�
    /// </summary>
    public void GameOver()
    {
        Debug.Log("��Ϸʧ��");
    }
    public void GameWin()
    {
        Debug.Log("��Ϸʤ��");
    }

}
