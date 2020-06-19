using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node : MonoBehaviour
{

    public bool isEnd;

    
    public virtual Node GetNextNode()
    {
        return null;
    }
}
