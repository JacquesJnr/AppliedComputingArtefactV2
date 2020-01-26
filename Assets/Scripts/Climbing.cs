using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Climbing : MonoBehaviour
{
    public Rigidbody2D torso, leftArm, rightArm;
    public GameObject left, right, foreArmLeft, foreArmRight;
    public GameObject leftTarget, rightTarget;
    private float radius = 0.5f;
    private string keyString;
    public KeyCode currentKey;
    public LayerMask WhatIsLeft, WhatIsRight;
    public bool leftHandAttached, rightHandAttached, noHands;
    private bool soundPlayed;

    private AudioManager myAudio;

    public List<string> inputs = new List<string>();

    private void Start()
    {
        Application.targetFrameRate = 300;
        myAudio = FindObjectOfType<AudioManager>();
        soundPlayed = false;
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
                leftTarget = GameObject.Find(currentKey.ToString());
                MoveLeft();
            }

            if (leftHandAttached)
            {
                //PlayOnce();
                if (Input.GetKey(keyString.ToLower()))
                {
                    rightTarget = GameObject.Find(currentKey.ToString());
                }

                if (rightTarget != leftTarget)
                {
                    MoveRight();
                }

                torso.bodyType = RigidbodyType2D.Dynamic;
            }

            if (rightHandAttached)
            {
                //PlayOnce();
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
                myAudio.Play("GruntStart");
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                myAudio.Play("GruntEnd");
            }

        }
    }

    private void GetKeyCode()
    {
        if (Input.anyKey)
        {
            var name = Input.inputString.ToUpper();
            currentKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), name);
        }
    }

    private void MoveLeft()
    {
        left.transform.position = Vector2.MoveTowards(left.transform.position, leftTarget.transform.position, 15f * Time.deltaTime);
        foreArmLeft.transform.position = Vector2.MoveTowards(foreArmLeft.transform.position, leftTarget.transform.position, 5f * Time.deltaTime);
        leftArm.AddForce(transform.up * 10f);
    }

    private void MoveRight()
    {
        right.transform.position = Vector2.MoveTowards(right.transform.position, rightTarget.transform.position, 15f * Time.deltaTime);
        foreArmRight.transform.position = Vector2.MoveTowards(foreArmRight.transform.position, rightTarget.transform.position, 5f * Time.deltaTime);
        rightArm.AddForce(transform.up * 10f);
    }


    private void Thrust()
    {
        torso.AddForce(Vector2.up * 65, ForceMode2D.Impulse);
    }

    private void PlayOnce()
    {
        
    }
}