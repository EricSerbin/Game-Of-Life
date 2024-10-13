
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Node[,] nodes;
    public List<Node> walls = new List<Node>();
    int[,] m_mapData;
    int m_width;
    int m_height;

    public int getWidth()
    {
        return m_width;
    }
    public void setWidth(int m_width)
    {
        this.m_width = m_width;
    }
    public int getHeight()
    {
        return m_height;
    }
    public void setHeight(int m_height)
    {
        this.m_height = m_height;
    }

    public static readonly Vector2[] allDirections =
    {
        new Vector2(0f,1f),
        new Vector2(1f,1f),
        new Vector2(1f,0f),
        new Vector2(1f,-1f),
        new Vector2(0f,-1f),
        new Vector2(-1f,-1f),  
        new Vector2(-1f,0f),
        new Vector2(-1f,1f)
    };

    public void Init(int[,] mapData)
    {
        m_mapData= mapData;
        m_width = mapData.GetLength(0);
        m_height = mapData.GetLength(1);
        nodes = new Node[m_width, m_height];

        for(int y=0;y<m_height;y++)
        {
            for(int x=0;x<m_width;x++)
            {

                NodeType type=(NodeType)mapData[x,y]; //our typecasting
                Node newNode = new Node(x, y, type);
                nodes[x,y]=newNode;//storing in array of nodes
                newNode.position = new Vector3(x, 0, y); //copying position from vector3
                

                if(type==NodeType.Alive)
                {
                    walls.Add(newNode);
                    
                }
            }
        }

        for (int y = 0; y < m_height; y++)
        {
            for (int x = 0; x < m_width; x++)
            {
                if (nodes[x,y].nodeType != NodeType.Alive)
                {
                    nodes[x, y].neighbors = GetModNeighbors(x,y); 
                }
                
            }
        }


    }
    public bool IsWithinBounds(int x, int y)
    {
        Debug.Log("x: " + x + "and y: " + y + " checked");
        return (x >= 0 && x < m_width && y >= 0 && y < m_height);
    }

    
    //new jazz
    List<Node> GetModNeighbors(int x, int y, Node[,] nodeArray, Vector2[] allDirections)
    {
        List<Node> neighborNodes = new List<Node>();
        foreach (Vector2 dir in allDirections)
        {
            int xHolder= (x + (int)dir.x);
            int yHolder = (y + (int)dir.y);
            
            int newX = ((xHolder % m_width)+m_width) % m_width;
            int newY = ((yHolder % m_height)+m_height) % m_height;//same as get neighbors but using modulo, accounting for negatives

            if (IsWithinBounds(newX, newY) && nodeArray[newX, newY] != null && nodeArray[newX, newY].nodeType != NodeType.Dead)
            {
                neighborNodes.Add(nodeArray[newX, newY]);
            }
        }
        return neighborNodes;
    }

    public List<Node> GetModNeighbors(int x, int y)
    {
        return GetModNeighbors(x, y, nodes, allDirections);
    }

}
