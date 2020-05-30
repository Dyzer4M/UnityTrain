using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;

public class Enemy :MonoBehaviour
{
    // Start is called before the first frame update

    private NavMeshAgent agent;

    public Node currentNode;

    private void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        setDesc(currentNode.nextNode.gameObject.transform.position);
    }
    void Update()
    {
        if ((Vector3.SqrMagnitude(this.gameObject.transform.position - agent.destination) < agent.stoppingDistance)){
            this.currentNode = this.currentNode.nextNode;
            if(this.currentNode.nextNode != null)
                setDesc(this.currentNode.nextNode.gameObject.transform.position);
        }
    }
    public void setDesc(Vector3 vec3)
    {
        agent.SetDestination(vec3);
    }

}
