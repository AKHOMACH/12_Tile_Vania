﻿using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Instance Variables
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f; // todo consider Vector2
    [SerializeField] float climbSpeed = 5f;

    [HideInInspector]
    public bool isOnLadder = false;

    bool playerHasVerticalSpeed;
    bool playerHasHorizontalSpeed;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    #endregion

    #region Messages
    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        VerticalMovement();
        HorizontalMovement();
        FlipSprite();
    }
    #endregion

    private void VerticalMovement()
    {
        if (isOnLadder)
        {
            ClimbLadder();
        }
        else
        {
            myAnimator.SetBool("Climbing", false);
            Jump();
        }
    }

    void HasVerticalSpeed()
    {
        playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        print(playerHasVerticalSpeed);
    }

    private void ClimbLadder()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);  // todo maybe force x to 0?
        myRigidBody.velocity = climbVelocity;

        
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump")) // Down so once per press
        {
            Vector2 jumpVelocityAdded = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityAdded;
        }
    }


    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            // reverse x scale to flip player horizontally
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    private void HorizontalMovement()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value between -1 and +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
     }

    
}
