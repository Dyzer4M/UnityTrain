using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleNextNode : Node
{
    [SerializeField]
    public List<Node> next;

    public override Node GetNextNode()
    {
        int index=Random.Range(0, next.Count * 1000)%next.Count;
        return next[index];
    }
}
