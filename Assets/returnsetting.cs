using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returnsetting : MonoBehaviour
{
    private static string previousScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            previousScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Setting");
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!string.IsNullOrEmpty(previousScene))
            {
                SceneManager.LoadScene(previousScene);
            }
        }
    }
    public void GoToSettings()
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Setting");
    }

    public void ReturnToPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
    }
}
