using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returnsetting : MonoBehaviour
{
    private static string previousScene;
    public void btnStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
    public void GoToSettings()
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Setting");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ReturnToPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
    }
}
