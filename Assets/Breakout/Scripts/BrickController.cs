using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BrickController : MonoBehaviour
{
    public SpriteRenderer SR;
    
    void Start()
    {
        //Register me to the list of all bricks that exist
        //Note that this uses the Static Variable we set up on BreakoutManager
        BreakoutManager.Me.AllBricks.Add(this);
        //Make yourself a random color
        SR.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    //This code makes the brick break
    public void Break()
    {
        //Destroy the brick
        //If we wanted to make any fancy effects, we could do that here
        Destroy(gameObject);
    }

    //This gets called by Unity when the object is destroyed
    private void OnDestroy()
    {
        //Remove me from the list of existing bricks
        BreakoutManager.Me.AllBricks.Remove(this);
    }
}
