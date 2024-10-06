using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    //My art
    public SpriteRenderer Body;
    
    //What's inside of me?
    public TileContentsController Contents;
    
    //Where am I?
    public int X;
    public int Y;

    //This gets called by GridManager when it spawns me
    public void Setup(int x, int y)
    {
        //Record where I am on the map
        X = x;
        Y = y;
    }

    //This function finds the tile that's X units to my right and Y above me
    //It's used for character movement--if a character wants to move one tile up they look for X:0,Y:1
    public TileController Neighbor(int x, int y)
    {
        //Ask the GridManager for the tile that's where I am, but with the requested offset
        return GridManager.Me.GetTile(X + x, Y + y);
    }
}
