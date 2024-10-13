using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class NextGeneration : MonoBehaviour
{
    public Graph my_graph;
    public PathFinder my_pathFinder;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(generate);
    }

    // Update is called once per frame
    void generate()
    {
        my_pathFinder.TraverseCells();
        my_pathFinder.SetNextCells();


        my_pathFinder.ShowColors();
    }
}
