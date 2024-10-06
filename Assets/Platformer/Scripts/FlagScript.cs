using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagScript : MonoBehaviour
{
    //My animator
    public Animator Anim;

    //My Audio Source & Sound Effects
    public AudioSource AS;
    public AudioClip VictorySFX;

    //Have I won? Used to avoid winning twice
    public bool Victory = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //If a player hits me and they haven't already won. . .
        PlayerScript ps = other.gameObject.GetComponent<PlayerScript>();
        if (ps != null && !Victory)
        {
            //Then I play my 'Spin' animation
            //The Spin animation automatically calls 'NextScene' at its end
            //Look at the animation itself to see how
            Anim.Play("Spin");
            //Also play the victory sound effect
            AS.PlayOneShot(VictorySFX);
            //Mark that I won. This prevents the victory sound from playing multiple times
            Victory = true;
        }
    }

    //Gets called automatically by the 'Spin' animation
    public void NextScene()
    {
        //If you add a second level, you should replace this code
          //with code that moves to the second level
        SceneManager.LoadScene("You Win");
        Anim.Play("Idle");
    }
}
