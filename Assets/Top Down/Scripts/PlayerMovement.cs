using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //Just a super simple movement script
    //We've done this before, so I won't comment it
    
    public static PlayerMovement Player;
    
    public Rigidbody2D RB;
    public float Speed = 5;
    public ProjectileController BulletPrefab;

    private void Awake()
    {
        //The one thing I do that's a little fancy is
          //I record the player to a static variable
          //so they're easy to find
        Player = this;
    }

    void Update()
    {
        //You've seen this movement code before
        Vector2 vel = Vector2.zero;
        if (Input.GetKey(KeyCode.D))
            vel.x = Speed;
        else if (Input.GetKey(KeyCode.A))
            vel.x = -Speed;
        if (Input.GetKey(KeyCode.W))
            vel.y = Speed;
        else if (Input.GetKey(KeyCode.S))
            vel.y = -Speed;
        RB.velocity = vel;
        
        //If I click, shoot!
        if (Input.GetMouseButtonDown(0))
        {
            //Okay, but where am I aiming? Let's find out where the mouse cursor is
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //This little bit of math calculates what direction the bullet should
            //  aim to be facing at the mouse cursor. Don't sweat the details
            float angle = Mathf.Atan2(pos.y-transform.position.y, pos.x-transform.position.x) * Mathf.Rad2Deg;
            //Spawn the projectile where the player is, and give it a Z-rotation of the above
            Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //If I walk into the exit. . .
        if (other.gameObject.CompareTag("Exit"))
        {
            //Win the game!
            SceneManager.LoadScene("You Win");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //If I walk into a monster or other hazard. . .
        if (other.gameObject.CompareTag("Hazard"))
        {
            //Lose the game!
            SceneManager.LoadScene("You Lose");
        }
    }
}
