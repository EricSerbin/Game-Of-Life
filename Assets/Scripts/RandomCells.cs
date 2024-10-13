using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomCells : MonoBehaviour
{
    // Start is called before the first frame update
    public Graph my_graph;
    public PathFinder my_pathFinder;
    void Start()
    {

        GetComponent<Button>().onClick.AddListener(clear);

    }

    // Update is called once per frame
    void clear()
    {
       
        Debug.Log("LOOBSETERBOISEHITHOAEJHFLAUHFADHIIH::HEIFLAKFHJFLKHE:JFHGIAEIJL:FHIDSHIFHI:ESLKF\n\n\n\n\n\n\n\n\n\n\n\n");
        Debug.Log("graph width is " + my_graph.getWidth() + "and graph height is " + my_graph.getHeight() + "\n");
        //my_graphView = GetComponent<GraphView>();

        for (int i = 0; i < my_graph.getWidth(); i++)
        {
            for(int j = 0; j < my_graph.getHeight(); j++)
            {
                bool randomValue = Random.value > .5;
                if (randomValue==true)
                {
                    my_graph.nodes[i, j].nodeType = NodeType.Alive;

                }
                else
                {
                    my_graph.nodes[i, j].nodeType = NodeType.Dead;
                }
            }
        }
        my_pathFinder.ShowColors();
    }
}
