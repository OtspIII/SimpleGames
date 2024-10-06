using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlayerController : TileContentsController
{
    //The script for the player
    //Note that it inherits from TileContentsController, so it has all its variables/functions
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //If I hit right, go right
            Move(1,0);
            //And then tell the Grid Manager that I took my turn and everyone else can take theirs
            GridManager.Me.TakeTurn();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(-1,0);
            GridManager.Me.TakeTurn();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(0,1);
            GridManager.Me.TakeTurn();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(0,-1);
            GridManager.Me.TakeTurn();
        }
    }

    //This gets called when I walk into a non-empty tile
    public override void HandleBump(TileController where, TileContentsController who)
    {
        //If the contents of the other tile are an enemy. . .
        if (who is GridEnemyController)
        {
            //Kill them
            who.Die();
            //And then step into their space
            SetTile(where);
        }
    }
}
