using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

using Color = UnityEngine.Color;

public class PathFinder : MonoBehaviour
{
    
    Graph m_graph;
    GraphView m_graphView;
    private float fixedDeltaTime;

    NodeView m_nodeView;
    MapData m_mapData;

    //Queue<Node> m_frontierNodes;
    Stack<Node> m_frontierNodes;
    List<Node> m_exploredNodes;
    List<Node> m_pathNodes;

    public bool isComplete = false;

   
    public Color aliveCellColor= Color.green;
    public Color deadCellColor = Color.black;
    public Color nextCellColor= Color.blue; //can also be cyan


    public void Init(Graph graph, GraphView graphView)
    {
        if (graph == null || graphView == null)
        {
            Debug.LogWarning("PATH finder init are missing components");
            return;
        }
        
        m_graph = graph;
        m_graphView = graphView;

        m_exploredNodes = new List<Node>();
        m_pathNodes = new List<Node>();

    }
    public void UpdateGraphView(Graph tempGraph)
    {
        Destroy(m_graphView);
        m_graphView.Init(tempGraph); //this was used for a method for resizing the grid at runtime, which was not feasible

    }

    private void ShowColors(GraphView graphView)
    {
     

        for (int i = 0; i < m_graph.getWidth(); i++)
        {
            for (int j = 0; j < m_graph.getHeight(); j++)
            {
                Node currentNode = m_graph.nodes[i, j];


                //Debug.Log("graph nodes comparison is " + m_graph.nodes[i, j].nodeType);
                if (m_graph.nodes[i, j].nodeType == NodeType.Alive && m_graph.nodes[i,j].newbornFlag==1)
                {
                    //Debug.Log("LOBSTER ALIVE");
                    graphView.nodeViews[i, j].ColorNode(nextCellColor);//blue
                }
                else if (m_graph.nodes[i, j].nodeType == NodeType.Alive)
                {
                    graphView.nodeViews[i, j].ColorNode(aliveCellColor);//green

                }
                else if (m_graph.nodes[i, j].nodeType == NodeType.Dead)//grey
                {
                    graphView.nodeViews[i, j].ColorNode(deadCellColor);

                }
                else
                {
                }

            }
        }


    }
    public void ShowColors()
    {
        ShowColors(m_graphView);
    }


    public IEnumerator SearchRoutine(float timeStep=0.1f)
    {
        yield return null;

        ShowColors(m_graphView);
        
        while (!isComplete) //can change this for each number of generations
        {
            yield return new WaitForSeconds(timeStep); //this wait is dependent on the game speed, with a minimum of 1x game speed

            TraverseCells(); //traverse cells counts the cells neighbors and updates the next nodes
            SetNextCells(); //updates cells
            ShowColors(m_graphView); //updates colors

        }
    }
    public void TraverseCells()
    {
        for (int i = 0; i < m_graph.getWidth(); i++)
        {
            for (int j = 0; j < m_graph.getHeight(); j++)
            {

                //Debug.Log(" loop i " + i + " loop j " + j + "is\n");

                Node newNode = m_graph.nodes[i, j];
              
                int neighborCount = m_graph.GetModNeighbors(i, j).Count();//this gets the neighbors of each cell, using modulos to wrap around edges
                int cellLogicResult = CellLogic(neighborCount, newNode.nodeType); //this uses the neighbor count for game states

                //Debug.Log("Plain getNeighbors:\n");
                //Debug.Log(m_graph.GetNeighbors(i, j).Count());

                //Debug.Log("Mod getNeighbors:\n");
                //Debug.Log((8 - m_graph.GetModNeighbors(i, j).Count())); //unit testing

                NodeType type = (NodeType)cellLogicResult;
                Node nextNode = new Node(i, j, type);
                
                newNode.next = nextNode;
                if (cellLogicResult == 1 && m_graph.nodes[i, j].nodeType == NodeType.Dead)
                {
                    nextNode.newbornFlag = 1; //if the cell is alive and was dead the previous round, it is a newborn and is flagged as such
                }
                m_graph.nodes[i, j] = newNode;

            }
        }

    }
    public void SetNextCells() //a simple update changing next nodes into current nodes
    {
        for (int i = 0; i < m_graph.getWidth(); i++)
        {
            for (int j = 0; j < m_graph.getHeight(); j++)
            {
                m_graph.nodes[i, j] = m_graph.nodes[i, j].next;
            }
        }

    }

    public int CellLogic(int var,  NodeType curState)
    {
        if (curState==NodeType.Alive && var < 2)
        {
            Debug.Log("cell has died of loneliness\n");
            return 0;
        }
        else if (curState==NodeType.Alive && var == 2)
        {
            Debug.Log("cell has maintained with 2 neighbors\n");
            return 1;
        }
        else if(curState==NodeType.Alive && var==3)
        {
            Debug.Log("cell has maintained with 3 neighbors\n");
            return 1;
        }
        else if (curState==NodeType.Alive && var > 3)
        {
            Debug.Log("cell has died of overcrowding\n");
            return 0;

        }
        else if(curState==NodeType.Dead && var==3)
        {
            Debug.Log("cell has been born\n");
            return 1;
        }
        else
        {
            Debug.Log("there are no valid neighbors, you are dead\n");
            return 0;
        }
    }
}
