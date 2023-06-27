using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public void EnablePanel(GameObject Panel)
    {
        Panel.SetActive(true);
    }
    public void DisablePanel(GameObject Panel)
    {
        Panel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene($"Scenes/{SceneName}");
    }
}
