using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //My Rigibody
    public Rigidbody2D RB;
    //My starting velocity. This should be set in the editor
    public Vector2 StartVel;
    //My starting position, where I respawn into. I set this in Start()
    public Vector3 StartPos;
    
    void Start()
    {
        //I record where I started, so I can respawn there
        StartPos = transform.position;
        //I check my StartVelocity variable and set that to be my velocity
        RB.velocity = StartVel;
    }

    void Update()
    {
        //If I'm off-screen, I respawn with my initial position & speed
        if (transform.position.y < -10)
        {
            transform.position = StartPos;
            RB.velocity = StartVel;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //If I hit something, I'm going to bounce. Let's calculate my new velocity
        Vector2 vel = RB.velocity;
        
        //Did I hit the paddle?
        PaddleController pc = other.gameObject.GetComponent<PaddleController>();
        if (pc != null)
        { 
            //If so, I should bounce back up
            vel.y *= -1;
            //I should also be aimed based on where I hit the paddle
            //I ask the paddle to calculate this for me
            vel.x = pc.BounceAngle(this);
        }

        //Did I hit a brick?
        BrickController bc = other.gameObject.GetComponent<BrickController>();
        if (bc != null)
        {
            //If so, I bounce vertically
            //MINOR BUG: if I hit a brick from the side I should bounce horizontally
            vel.y *= -1;
            //Also I tell the brick to break
            bc.Break();
        }

        //If I hit a vertical wall, I bounce horizontally
        if (other.gameObject.CompareTag("VWall"))
        {
            vel.x *= -1;
        }
        
        //If I hit a horizontal wall (the roof), I bounce vertically
        if (other.gameObject.CompareTag("HWall"))
        {
            vel.y *= -1;
        }

        //Now that I've calculated any bouncing I need to do, plug that into my rigidbody
        RB.velocity = vel;
    }
}
