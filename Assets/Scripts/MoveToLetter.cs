using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLetter : MonoBehaviour
{
    public float speed;
    private string input;
    private GameObject leftTarget, rightTarget, armTarget;




    private void Update()
    {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) //Get the current key being pressed
        {
            if (Input.GetKey(vKey)) //If any key is pressed
            {
                input = vKey.ToString(); //Convert the keycode of the pressed key to a string

                armTarget = GameObject.Find(input);// Find the block with the corresponding name

                transform.position = Vector2.MoveTowards(transform.position, armTarget.transform.position, speed * Time.deltaTime); // Move the player to that gameobject
            }
        }
    }
}
