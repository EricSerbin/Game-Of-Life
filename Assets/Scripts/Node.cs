
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NodeType
{
    Dead = 0,
    Alive= 1
}

public class Node 
{
    public NodeType nodeType = NodeType.Dead;
    public int xIndex = -1;
    public int yIndex = -1;

    public Vector3 position;
    public List<Node> neighbors=new List<Node>();
    public Node next = null;
    public int newbornFlag=0;
    public Node(int xIndex, int yIndex, NodeType nodeType)
    {
        this.xIndex = xIndex;
        this.yIndex = yIndex;
        this.nodeType = nodeType;
        
    }

}

