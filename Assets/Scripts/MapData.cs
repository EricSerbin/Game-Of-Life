
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    
    [Range(1, 50)]
    public int width = 1;
    [Range(1, 50)]
    public int height = 1; 
    //lets user adjust dimensions

    public int[,] MakeMap() //public int [,]
    {
        int[,] map = new int[width, height];
        for(int y=0;y<height; y++)
        {
            for(int x=0;x<width;x++)
            {
                map[x, y] = 0;//[x,y];
            }
        }
           
        
        if(width>11 && height > 7)
        { //some flippers to show the game has initialized well
            map[4, 2] = 1;
            map[4, 3] = 1;
            map[4, 4] = 1;
            map[7, 0] = 1;
            map[7, 1] = 1;
            map[7, 6] = 1;
            map[11, 2] = 1;
            map[10, 2] = 1;
            map[0, 2] = 1;


        }

        return map;
    }

    void DisplayMap(int[,] map) 
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Debug.Log(map[x, y]);
                //Console.WriteLine(map[x, y]);
            }
        }

    }
    
    public void Start()
    {
        int[,] mapInstance= MakeMap(); 
        DisplayMap(mapInstance);

    }

}
