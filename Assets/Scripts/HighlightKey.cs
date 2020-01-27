using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightKey : MonoBehaviour
{
    public Sprite selected, unselected;
    private KeyCode myKeyCode;
    private string objName;


    private void FixedUpdate()
    {
        GetKey();
    }

    void Update()
    {
        objName = gameObject.name;

        if (Input.GetKey(objName.ToLower()))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = selected;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = unselected;
        }

        
    }

    void GetKey()
    {
        var name = Input.inputString.ToUpper();
        myKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), name);
    }
}
