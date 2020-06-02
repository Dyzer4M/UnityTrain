using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Enemy :MonoBehaviour
{
    // Start is called before the first frame update
    public Node currentDesc;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        
        setDesc(currentDesc.gameObject.transform.position);
    }
    void Update()
    {
        Move();
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
        EnemyManager.EnemyAliveCount--;

        EnemyHealth health = gameObject.GetComponent<EnemyHealth>();


        if (health.isAlive())
            Destroy(gameObject);
    }
    
}
