using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    public float Speed;
    public float ScreenBounds;
    
    void Update()
    {
        //Calculate what my position should be
        //I don't use a rigidbody because this isn't physics movement
        //The only thing in this game with a RB is the ball
        Vector3 pos = transform.position;
        
        //If I hit left, go left
        if (Keyboard.current.leftArrowKey.isPressed)
            pos += new Vector3(-Speed * Time.deltaTime, 0, 0);
        //If I hit right, go right
        else if (Keyboard.current.rightArrowKey.isPressed)
            pos += new Vector3(Speed * Time.deltaTime, 0, 0);
        
        //If I go off the edges of the screen, don't
        if (pos.x > ScreenBounds || pos.x < -ScreenBounds)
            pos.x = Mathf.Clamp(pos.x, -ScreenBounds, ScreenBounds);
        
        //Plug in the position I calculated to my transform
        transform.position = pos;

    }

    //What X velocity should the ball have when it hits the paddle?
    //AKA-how does aiming with the paddle work
    public float BounceAngle(BallController ball)
    {
        //Currently, the ball keeps its old X velocity
        //This is an intentionally bad answer--can you fix it?
        return ball.RB.linearVelocity.x;
    }
}
