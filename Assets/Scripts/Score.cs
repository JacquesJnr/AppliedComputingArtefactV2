using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float distance;
    private Vector2 prevPos;
    public GameObject origin, player;
    private Text myScore;

    private void Start()
    {
        prevPos = player.transform.position;
    }

    private void FixedUpdate()
    {
        if (player.transform.position.y > prevPos.y)
        {
            myScore = GetComponent<Text>();
            distance = player.transform.position.y - origin.transform.position.y;
            if(distance > 0)
            {
                myScore.text = distance.ToString("F2") + "m";
            }
           
        }

        prevPos = player.transform.position;
    }
}
