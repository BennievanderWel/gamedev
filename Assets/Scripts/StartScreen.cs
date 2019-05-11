using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{

    public string preferredNextScene = "Level 1";

    public void StartGame()
    {
        SceneManager.LoadScene(preferredNextScene, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
