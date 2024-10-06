using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile3DController : MonoBehaviour
{
    //My components
    public Rigidbody RB;
    
    //How fast do I fly?
    public float Speed = 30;
    //How hard do I knockback things I hit?
    public float Knockback = 10;

    void Start()
    {
        //When I spawn, I fly straight forwards at my Speed
        RB.velocity = transform.forward * Speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        //If I hit something with a rigidbody. . .
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            //I push them in the direction I'm flying with a power equal to my Knockback stat
            rb.AddForce(RB.velocity.normalized * Knockback,ForceMode.Impulse);
        }
        //If I hit anything, I despawn
        Destroy(gameObject);
    }
}
