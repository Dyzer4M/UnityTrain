using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public class SingNextNode : Node
{
    public Node next;

    public  Node NextNode
    {
        get
        {
            Type type = next.GetType();
            if (type.ToString() == "WaveSinNextNode")
            {
                WaveSinNextNode waveSinNextNode = next as WaveSinNextNode;
                if (waveSinNextNode.isInfectd && !waveSinNextNode.isEnd)
                {
                    return next.GetNextNode();
                }
            }
            
            return next;
        }
    }
    
    
    
    public override Node GetNextNode()
    {
        return NextNode;
    }

}
