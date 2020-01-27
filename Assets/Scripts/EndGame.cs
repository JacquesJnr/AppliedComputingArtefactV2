using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{ 
    private Collider2D bCollider;
    public LayerMask left, right;
    public bool winner;
    public GameObject winScreen;

    private void Start()
    {
        bCollider = GetComponent<Collider2D>();
        winScreen.SetActive(false);
    }

    private void Update()
    {
        winner = Physics2D.OverlapCircle(transform.position, 1f, left);

        if (winner)
        {
            winScreen.SetActive(true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
