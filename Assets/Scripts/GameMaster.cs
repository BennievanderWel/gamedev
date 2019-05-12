using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public string preferredNextScene = "Level 1";

    public bool isPaused = false;
    public Canvas pauseCanvas;

    void Start()
    {

    }
    
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused == false) {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
            
        }
    }


    public void LoadScene()
    {
        SceneManager.LoadScene(preferredNextScene, LoadSceneMode.Single);
    }

    public void StartGame()
    {
        LoadScene();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseCanvas.enabled = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseCanvas.enabled = false;
        Time.timeScale = 1;
    }
}
