using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public bool onFloor;
    private float radius = 1f;
    public LayerMask WhatIsPlayer;

    private void FixedUpdate()
    {
        onFloor = Physics2D.OverlapBox(transform.position, new Vector2(2f, 1.5f), 0, WhatIsPlayer);
       
        if (onFloor)
        {
           
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(2f, 1.5f));        
    }
}
