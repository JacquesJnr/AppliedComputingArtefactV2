using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Climbing : MonoBehaviour
{
    public Rigidbody2D torso, leftArm, rightArm; // The limbs being controlled by physics
    public GameObject left, right, foreArmLeft, foreArmRight; // The limbs being controlled by Vector2
    public GameObject leftTarget, rightTarget; //Target positions of each hand
    private float radius = 0.5f; // Radius of the overlap circle
    private string keyString; //Current keycode as a string
    public KeyCode currentKey; //Current keycode 
    public LayerMask WhatIsLeft, WhatIsRight; // Layer masks to check for the player's hands
    public bool leftHandAttached, rightHandAttached, noHands;
    private bool soundPlayed;

    private AudioManager myAudio; // My audio manager class

    public List<string> inputs = new List<string>();

    // Sets the desired frame rate and gives a reference to the audio class
    private void Start()
    {
        Application.targetFrameRate = 300;
        myAudio = FindObjectOfType<AudioManager>();
        soundPlayed = false;
        myAudio.Play("Ambient");
    }

    private void FixedUpdate()
    {
        GetKeyCode();

        if (leftTarget != null)
        {
            leftHandAttached = Physics2D.OverlapCircle(leftTarget.transform.position, radius, WhatIsLeft);
        }

        if (rightTarget != null)
        {
            rightHandAttached = Physics2D.OverlapCircle(rightTarget.transform.position, radius, WhatIsRight);
        }

        if (!leftHandAttached && !rightHandAttached)
        {
            noHands = true;
        }
        else
        {
            noHands = false;
        }

        keyString = currentKey.ToString(); 
        var lowerLeft = leftTarget.name.ToString();
        var lowerRight = rightTarget.name.ToString();

        // Sets the current hand's rigidbody to static if the player is holding the current key
        if (leftTarget != null)
        {
            if (leftHandAttached && Input.GetKey(lowerLeft.ToLower()))
            {
                left.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
            else
            {
                left.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }

        if (rightTarget != null)
        {
            if (rightHandAttached && Input.GetKey(lowerRight.ToLower()))
            {
                right.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
            else
            {
                right.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }

        // Stabilises the hinge joints on the player's limbs
        left.GetComponent<Rigidbody2D>().centerOfMass = Vector2.zero;
        left.GetComponent<Rigidbody2D>().inertia = 1.0f;
        leftArm.centerOfMass = Vector2.zero;
        leftArm.inertia = 1.0f;

        right.GetComponent<Rigidbody2D>().centerOfMass = Vector2.zero;
        right.GetComponent<Rigidbody2D>().inertia = 1.0f;
        rightArm.centerOfMass = Vector2.zero;
        rightArm.inertia = 1.0f;
    }

    private void Update()
    {
        if (Input.GetKey(keyString.ToLower()))
        {
            if (noHands)
            {
                leftTarget = GameObject.Find(currentKey.ToString()); // Set the left target to the current keycode as a string
                MoveLeft();
            }

            if (leftHandAttached)
            {
                if (Input.GetKey(keyString.ToLower()))
                {
                    rightTarget = GameObject.Find(currentKey.ToString()); // Set the right target to the current keycode as a string
                }

                if (rightTarget != leftTarget) //Checks that current target and the previous target aren't the same
                {
                    MoveRight();
                }

                torso.bodyType = RigidbodyType2D.Dynamic; // The torso is static on start, when the player starts climbing make it move
            }

            if (rightHandAttached)
            {
                if (Input.GetKey(keyString.ToLower()))
                {
                    leftTarget = GameObject.Find(currentKey.ToString());
                }

                if (leftTarget != rightTarget)
                {
                    MoveLeft();
                }
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Thrust();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                myAudio.Play("GruntStart"); // Plays audio from the audio class 
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                myAudio.Play("GruntEnd");
            }

        }
    }

    //Gets a string of the current key being pressed and converts to a keycode
    private void GetKeyCode()
    {
        if (Input.anyKey)
        {
            var name = Input.inputString.ToUpper();
            currentKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), name);
        }
    }

    //Move the left arm towards the letter being pressed
    private void MoveLeft()
    {
        left.transform.position = Vector2.MoveTowards(left.transform.position, leftTarget.transform.position, 12f * Time.deltaTime);
        foreArmLeft.transform.position = Vector2.MoveTowards(foreArmLeft.transform.position, leftTarget.transform.position, 12f * Time.deltaTime);
        //leftArm.AddForce(transform.up * 10f);
    }

    //Move the right arm towards the letter being pressed
    private void MoveRight()
    {
        right.transform.position = Vector2.MoveTowards(right.transform.position, rightTarget.transform.position, 12f * Time.deltaTime);
        foreArmRight.transform.position = Vector2.MoveTowards(foreArmRight.transform.position, rightTarget.transform.position, 12f * Time.deltaTime);
        //rightArm.AddForce(transform.up * 10f);
    }

    //Move the torso upwards
    private void Thrust()
    {
        torso.AddForce(Vector2.up * 40f, ForceMode2D.Impulse);
    }
}