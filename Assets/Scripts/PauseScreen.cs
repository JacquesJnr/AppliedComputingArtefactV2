using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public GameObject pausescreen;
    public GameObject winScreen;
    public GameObject cont, quit;
    public GameObject cam1, cam2, mainMenuCam;

    private void Start()
    {
        pausescreen.SetActive(false);
        cont.SetActive(false);
        quit.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            pausescreen.SetActive(true);
            cont.SetActive(true);
            quit.SetActive(true);
        }
    }

    public void Resume()
    {
        pausescreen.SetActive(false);
        cont.SetActive(false);
        quit.SetActive(false);
    }

    public void MainMenu()
    {
        pausescreen.SetActive(false);
        cont.SetActive(false);
        quit.SetActive(false);
        winScreen.SetActive(false);
        mainMenuCam.SetActive(true);
        cam1.SetActive(false);
        cam2.SetActive(false);
    }

    public void StartGame()
    {
        cam1.SetActive(true);
        mainMenuCam.SetActive(false);

        if (cam2.activeSelf)
        {
            cam2.transform.position = new Vector3(0.8190084f, -0.1785337f, -10f);
            cam2.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
