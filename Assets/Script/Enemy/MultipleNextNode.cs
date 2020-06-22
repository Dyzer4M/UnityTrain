using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class MultipleNextNode : Node
{
    [SerializeField]
    public List<Node> next;

    public  Node NextNode
    {
        get
        {
            int index = UnityEngine.Random.Range(0, next.Count * 1000)%next.Count;
            Type type = next[index].GetType();
            Debug.Log(type);
            if (type.ToString() == "WaveSinNextNode")
            {
                WaveSinNextNode waveSinNextNode = next[index] as WaveSinNextNode;
                if (waveSinNextNode.isInfectd && !waveSinNextNode.isEnd)
                {
                    return next[index].GetNextNode();
                }
            }
            
            return next[index];
        }
    }
    public override Node GetNextNode()
    {
        Debug.Log(NextNode.gameObject.name);
        return NextNode;
    }
}
