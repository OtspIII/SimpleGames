using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridEnemyController : TileContentsController
{
    //The script for the enemies
    //Note that it inherits from TileContentsController, so it has all its variables/functions
    
    //The directions I might move
    public List<Vector2Int> Dirs = new List<Vector2Int>();

    void Start()
    {
        //Build the list of directions I might move
        Dirs.Add(new Vector2Int(1,0));//Right
        Dirs.Add(new Vector2Int(-1,0));//Left
        Dirs.Add(new Vector2Int(0,1));//Up
        Dirs.Add(new Vector2Int(0,-1));//Down
    }
    
    //My AI code for what I do on my turn
    //Called by GridManager after the player takes an action
    public void TakeTurn()
    {
        //Pick one of the directions I can move
        Vector2Int dir = Dirs[Random.Range(0, Dirs.Count)];
        //And move in that direction
        Move(dir.x,dir.y);
    }

    //If I bump into something
    public override void HandleBump(TileController where, TileContentsController who)
    {
        //If I bumped into the player. . .
        if (who is GridPlayerController)
        {
            //Kill them
            who.Die();
            //And move into their old tile
            SetTile(where);
        }
        
    }

    //If I die, remove me from the list of enemies that exist
    private void OnDestroy()
    {
        GridManager.Me.AllEnemies.Remove(this);
    }
}
