using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool forwardPressed = Input.GetKey("w");
        bool isRunning = animator.GetBool("isRunning");
        bool runPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKey("space");
        bool isJumping = animator.GetBool("isJumping");

        //if player presses w key
        if (!isWalking && forwardPressed)
        {
            //then set isWalking boolean to be true
            animator.SetBool("isWalking", true);
        }

        //if player is not pressing w key
        if (isWalking && !forwardPressed)
        {
            //then set isWalking boolean to be false
            animator.SetBool("isWalking", false);
        }

        //if player is walking and presses left shift
        if(!isRunning &&(forwardPressed && runPressed))
        {
            //then se the isRunning boolean to be true
            animator.SetBool("isRunning", true);
        }

        //if player is running and stops running or stops walking
        if (isRunning && (!forwardPressed || !runPressed))
        {
            //then  set the isRunning boolean to be false
            animator.SetBool("isRunning", false);
        }

        if (jumpPressed && isRunning)
        {
            animator.SetBool("isJumping", true);
        }

       // if(isJumping && !jumpPressed)
    }
}
