using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public bool onFloor, reset;
    private float radius = 1f;
    public GameObject fadeIn, fadeOut;
    public LayerMask WhatIsPlayer;
    public Rigidbody2D torso;
    public Rigidbody2D leftArm, rightArm;
    public GameObject cam1, cam2;

    private void Start()
    {
        fadeIn.SetActive(false);
        fadeOut.SetActive(false);
        cam2.SetActive(false);
    }

    // Checks to see if the player's torso is on the floor
    private void FixedUpdate()
    {
        onFloor = Physics2D.OverlapBox(transform.position, new Vector2(2f, 1.5f), 0, WhatIsPlayer);

        //Fade out if the player's torso is touching the floor
        if (onFloor)
        {
            fadeOut.SetActive(true);
        }

        //Fade In if the fade out screen is active
        if (fadeOut.activeSelf == true)
        {
            StartCoroutine(FadeIn());
        }

    }

    //Waits before Fading in
    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(2f); 
        fadeOut.SetActive(false);
        ResetPlayerPos();
        fadeIn.SetActive(true);
    }

    //Resets the player's position while the screen is black
    public void ResetPlayerPos()
    {
        torso.bodyType = RigidbodyType2D.Static;
        torso.transform.position = new Vector2(-0.1198142f, -0.01295245f);
        torso.SetRotation(0);

        if(leftArm.velocity.y > 0)
        {
            leftArm.velocity = Vector2.zero;
        }

        if(rightArm.velocity.y > 0)
        {
            rightArm.velocity = Vector2.zero;
        }

        if (cam1.activeSelf)
        {
            cam2.SetActive(true);
            cam1.transform.position = new Vector3(0.8190084f, -0.1785337f, -10f);
            cam1.SetActive(false);
        }

        if (cam2.activeSelf)
        {
            cam1.SetActive(true);
            cam2.transform.position = new Vector3(0.8190084f, -0.1785337f, -10f);
            cam2.SetActive(false);
        }
    }
}
