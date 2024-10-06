using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //My Components
    public SpriteRenderer SR;
    public Rigidbody2D RB;
    public Collider2D Coll; //My main collider
    public Collider2D Nose; //My 'did I walk into a wall' collider
    
    //My personal stats
    public float Speed = 2;
    public float Gravity = 3;
    
    //Variables I use to track my state
    public bool OnGround = false;
    public bool FacingLeft = false;
    
    void Start()
    {
        //When I come into existence, let the GM know
        GameManager.Me.AllEnemies.Add(this);
    }

    void Update()
    {
        //I'll use this variable to track the movement I want
        //By default, I move like I moved last frame
        Vector2 vel = RB.velocity;

        //If I'm facing right, move right. . .
        if (!FacingLeft)
        { 
            vel.x = Speed;
        }
        else //Otherwise, move left
        { 
            vel.x = -Speed;
        }

        //Here I actually feed the Rigidbody the movement I want
        RB.velocity = vel;

        //If I fall off the bottom of the screen, self destruct
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //If I hit something, and the collider of mine that
          //hit it was my nose, turn around
        if (other.otherCollider == Nose)
        {
            TurnAround();
        }
    }

    //Toggle me to turn around
    public void TurnAround()
    {
        //Make FacingLeft be the opposite of what it was
        FacingLeft = !FacingLeft;
        //Use my FacingLeft variable to turn my whole body around
        if(FacingLeft)
            transform.localScale = new Vector3(-1,1,1);
        else
            transform.localScale = new Vector3(1,1,1);
    }

    private void OnDestroy()
    {
        //When I die, remove me from the list of enemies on the GM
        GameManager.Me.AllEnemies.Remove(this);
    }
}
