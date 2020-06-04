using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


/// <summary>
/// 已经派生出不同怪物的类，若需要修改最好按需修改这个类或者修改其子类
/// </summary>
public class Enemy :MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public  Node currentDesc;
    private NavMeshAgent agent;
    public Action<Enemy> aliveCallback;
    public Action<Enemy> deadCallback;

    [HideInInspector]
    public bool isArrive = false;

    protected void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();

    }
    protected virtual void Start()
    {
       // EnemyManager.EnemyAliveCount++;
        setDesc(currentDesc.gameObject.transform.position);
        if(aliveCallback!=null)
        {
            Enemy enemy = GetComponent<Enemy>();
            aliveCallback(enemy);
        }
    }
    protected virtual void Update()
    {
        EnemyHealth enemyHp = gameObject.GetComponent<EnemyHealth>();
        if (enemyHp.isAlive())
        {
            
            Move();//在活着的时候才动

        }
        else
        {
            agent.speed = 0;//原地死亡
        }


    }


    protected  virtual  void OnDestroy()
    {
        //EnemyManager.EnemyAliveCount--;
        if(deadCallback!=null)
        {
            Enemy enemy = GetComponent<Enemy>();
            deadCallback(enemy);
        }
    }

    void Move()
    {
        //到达终点
        if (currentDesc.isEnd && (Vector3.SqrMagnitude(this.gameObject.transform.position - agent.destination) < agent.stoppingDistance))
        {
            EnemyArriveEnd();
        }
        if (!currentDesc.isEnd && (Vector3.SqrMagnitude(this.gameObject.transform.position - agent.destination) < agent.stoppingDistance))
        {
            this.currentDesc = this.currentDesc.nextNode;
            Debug.Log(currentDesc);
            setDesc(this.currentDesc.gameObject.transform.position);
        }

    }
    public void setDesc(Vector3 vec3)
    {
        agent.SetDestination(vec3);
    }

    //怪物抵达终点所做处理
    void EnemyArriveEnd()
    {
       // EnemyManager.EnemyAliveCount--;

        EnemyHealth health = gameObject.GetComponent<EnemyHealth>();


        if (health.isAlive())
        {
            isArrive = true;
            Destroy(gameObject);
            
        }
    }
    
}
