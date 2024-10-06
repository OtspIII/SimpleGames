using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContentsController : MonoBehaviour
{
    //A generic script for anything that can live inside of a tile
    //GridPlayerController and GridEnemyController both inherit from this
    //That means they inherit all of my variables and functions
    
    //My art
    public SpriteRenderer SR;
    //The tile I'm currently inside of
    public TileController Tile;

    //This function moves me in a direction
    //I use this to move instead of ever touching transform.position or rb.velocity
    public void Move(int x, int y)
    {
        //If I'm not already in a tile, I can't move to an adjacent tile because there are none
        if (Tile == null)
        {
            Debug.Log("ERROR: " + gameObject + " TRIED TO MOVE DESPITE NOT EXISTING ANYWHERE");
            return;
        }
        //Find the tile exists x squares to my left and y squares above me
        TileController dest = Tile.Neighbor(x, y);
        //If there is no tile there (it's off the side of the map?) abort my attempt to move
        if (dest == null)
        {
            return;
        }

        //If the tile I want to move into already has something inside of it
        if (dest.Contents != null)
        {
            //Call the HandleBump function to handle exactly what happens,
              //and tell it both where I was going and who's there
            HandleBump(dest,dest.Contents);
        }
        else
        {
            //If the tile's empty, just move into it
            SetTile(dest);
        }
    }

    //This gets called when I bump into another object in a tile I'm trying to move into
    //It's virtual, so that means the scripts that inherit from me will override it with new code
    //That's why it's okay that it's totally empty for me
    public virtual void HandleBump(TileController where, TileContentsController who)
    {
        
    }

    //All the things that need to happen when I move into a tile
    public void SetTile(TileController where)
    {
        //If I try to move into a tile that already has something in it, abort the process
        if (where.Contents != null)
        {
            Debug.Log("ERROR: " + gameObject + " TRIED TO GO INTO A NON-EMPTY TILE");
            return;
        }
        //If I was already in a tile, tell it that I'm leaving
        if(Tile != null) LeaveTile();
        //Set my tile to my new location
        Tile = where;
        //Tell my new tile that I'm in it
        Tile.Contents = this;
        //Teleport me in front of my tile
        transform.position = Tile.transform.position;

    }

    public void LeaveTile()
    {
        //If I'm not in a tile, I don't need to do this
        if (Tile == null) return;
        //Safety check--if the tile lists me as what's in it, then mark it as empty
        if(Tile.Contents == this)
            Tile.Contents = null;
    }

    //Code for being destroyed
    public void Die()
    {
        //Mark my tile as empty
        LeaveTile();
        //And destroy my game object
        Destroy(gameObject);
    }
    
}
