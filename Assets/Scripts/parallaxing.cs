using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxing : MonoBehaviour
{
    public List<Transform> backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;
    private Transform cam;
    private Vector3 previousCamPos;

    void Awake()
    {
        // set up camera the reference
        cam = Camera.main.transform;

        //Add the background images that will make up the parallax using the background tag
        foreach (GameObject bg in GameObject.FindGameObjectsWithTag("background"))
        {
            backgrounds.Add(bg.GetComponent<Transform>());
        }
    }
    
    void Start()
    {
        // The previous frame had the current frame's camera position
        previousCamPos = cam.position;
        // asigning coresponding parallaxScales
        parallaxScales = new float[backgrounds.Count];
        for (int i = 0; i < backgrounds.Count; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z;
        }
    }
   
    void Update()
    {
        // for each background
        for (int i = 0; i < backgrounds.Count; i++)
        {
            // the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
            float parallax = (cam.position.x - previousCamPos.x) * parallaxScales[i];
            // set a target x position which is the current position plus the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            float backgroundTargetPosY = backgrounds[i].position.y + parallax;
            // create a target position which is the background's current position with it's target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            // fade between current position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        // set the previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;


        //Controls to test the parallax effect

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * 5f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * 5f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector2.up * 5f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector2.down * 5f * Time.deltaTime);
        }
    }
}
