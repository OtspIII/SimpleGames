using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    //The camera is inside the player
    public Camera Eyes;
    
    public Rigidbody RB;
    public Projectile3DController ProjectilePrefab;
    
    //Character stats
    public float MouseSensitivity = 3;
    public float WalkSpeed = 10;
    public float JumpPower = 7;
    
    //A list of all the solid objects I'm currently touching
    public List<GameObject> Floors;

    
    
    void Start()
    {
        //Turn off my mouse and lock it to center screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        //If my mouse goes left/right my body moves left/right
        float xRot = Input.GetAxis("Mouse X") * MouseSensitivity;
        transform.Rotate(0,xRot,0);
        
        //If my mouse goes up/down my aim (but not body) go up/down
        float yRot = -Input.GetAxis("Mouse Y") * MouseSensitivity;
        Eyes.transform.Rotate(yRot,0,0);

        //Movement code
        if (WalkSpeed > 0)
        {
            //My temp velocity variable
            Vector3 move = Vector3.zero;
            
            //transform.forward/right are relative to the direction my body is facing
            if (Input.GetKey(KeyCode.W))
                move += transform.forward;
            if (Input.GetKey(KeyCode.S))
                move -= transform.forward;
            if (Input.GetKey(KeyCode.A))
                move -= transform.right;
            if (Input.GetKey(KeyCode.D))
                move += transform.right;
            //I reduce my total movement to 1 and then multiply it by my speed
            move = move.normalized * WalkSpeed;
            
            //If I hit jump and am on the ground, I jump
            if (JumpPower > 0 && Input.GetKeyDown(KeyCode.Space) && OnGround())
                move.y = JumpPower;
            else  //Otherwise, my Y velocity is whatever it was last frame
                move.y = RB.velocity.y;
            
            //Plug my calculated velocity into the rigidbody
            RB.velocity = move;
        }

        //If I click. . .
        if (Input.GetMouseButtonDown(0))
        {
            //Spawn a projectile right in front of my eyes
            Instantiate(ProjectilePrefab, Eyes.transform.position + Eyes.transform.forward,
                Eyes.transform.rotation);
        }
    }

    //I count as being on the ground if I'm touching at least one solid object
    //This isn't a perfect way of doing this. Can you think of at least one way it might go wrong?
    public bool OnGround()
    {
        return Floors.Count > 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        //If I touch something and it's not already in my list of things I'm touching. . .
            //Add it to the list
        if (!Floors.Contains(other.gameObject))
            Floors.Add(other.gameObject);
    }

    private void OnCollisionExit(Collision other)
    {
        //When I stop touching something, remove it from the list of things I'm touching
        Floors.Remove(other.gameObject);
    }
}