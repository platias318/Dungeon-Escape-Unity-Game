using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoDimensionalAnimationCrouched : MonoBehaviour
{
    Animator animator;
    float velocityZ = 0.0f; // Y axis in blent tree graph
    float velocityX = 0.0f; // X axis in blent tree graph
    public float acceleration = 2.0f; // when the player is pressing the key down
    public float deceleration = 2.0f; //when the player is not pressing the key down
    public float maximumWalkVelocity = 0.5f;

    //increase perfomance
    int VelocityZHash;
    int VelocityXHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");


    }

    void changeVelocity(bool forwardPressed, bool backPressed, bool leftPressed, bool rightPressed)
    {
        // if player presses forward, increase velocity in z direction
        if (forwardPressed && velocityZ < maximumWalkVelocity) //continue increasing the velocity as long as it doesnt reach the maximum
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        //if player pressed back , decrease velocity in z direction
        if (backPressed && velocityZ > -maximumWalkVelocity) //continue decreasing the velocity as long as it doesnt reach the minimum
        {
            velocityZ -= Time.deltaTime * acceleration;

        }

        //increase velocity in left direction
        if (leftPressed && velocityX > -maximumWalkVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        // increase velocity in right direction
        if (rightPressed && velocityX < maximumWalkVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        // decrease velocityZ if forward not pressed so it reaches the idle state at some point
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
        //increase velocityZ if back is not pressed so it reaches the idle state at some point
        if (!backPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }

        //increase velocityX if left is not pressed and velocityX < 0
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        //decrease velocityX if right is not pressed and velocityX > 0
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    //handles reset and locking of velocity
    void lockOrResetVelocity(bool forwardPressed, bool backPressed, bool leftPressed, bool rightPressed)
    {
        // reset velocityZ
        if (!forwardPressed && !backPressed && velocityZ != 0 && (velocityZ > -0.05 && velocityZ < 0.05f)) //from [-0.05,0.05]
        {
            velocityZ = 0.0f;
        }

        // reset velocityX
        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f)) //from [-0.05,0.05]
        {
            velocityX = 0.0f;
        }

        //lock forward

    }

    // Update is called once per frame
    void Update()
    {
        //input will be true if the player is pressing on the passed in key parameter 
        // get key input from player
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backPressed = Input.GetKey("s");
        bool downPressed = Input.GetKey("c");


        //handles changes in velocity
        changeVelocity(forwardPressed, backPressed, leftPressed, rightPressed);
        lockOrResetVelocity(forwardPressed, backPressed, leftPressed, rightPressed);



        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
        animator.SetBool("isCrouching", downPressed);
    }
}
