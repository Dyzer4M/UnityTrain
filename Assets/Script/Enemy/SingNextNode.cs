using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingNextNode : Node
{
    public Node next;

    public override Node GetNextNode()
    {
        return next;
    }

}
