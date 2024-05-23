using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class makevideo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject loadingScreenPrefab;
    public string fullText;
    private GameObject loadingScreenInstance;
    private Text textComponent;
    private int currentIndex;
    private float nextCharacterTime;
    private float loadSceneTime;
    private bool loadingScene;
    public Canvas canvas;

    // Add your Google Cloud Text-to-Speech API key here
    //private string apiKey = " AIzaSyCw2ADCx3zfAY5kI2XhxoohSRaMu4BU9to";

    public void StartDisplayTextAndLoadScene()
    {
        loadingScene = false; // Initialize loadingScene to false
        loadingScreenInstance = Instantiate(loadingScreenPrefab);
        loadingScreenInstance.SetActive(true);

        Camera.main.gameObject.SetActive(false);

        // Get Text component from prefab
        textComponent = loadingScreenInstance.GetComponentInChildren<Text>();
        if (textComponent == null)
        {
            Debug.LogError("Prefab does not have Text component!");
            return;
        }

        // Set initial text for textComponent
        //textComponent.text = fullText;
        //canvas = loadingScreenInstance.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.enabled = false;
        }
        currentIndex = 0;
        nextCharacterTime = Time.time; // Time to start displaying the first character
        loadSceneTime = Time.time + 5f; // Time to load the new scene
    }

    private void Update()
    {
        // If there are still characters to display and it's time to display the next character
        if (currentIndex < fullText.Length && Time.time >= nextCharacterTime)
        {
            textComponent.text = fullText.Substring(0, currentIndex + 1); // Display from start to current character
            currentIndex++;
            nextCharacterTime += 0.3f; // Time between each character
        }
        // If all text has been displayed and not loading scene yet and it's time to load scene
        else if (currentIndex >= fullText.Length && !loadingScene && Time.time >= loadSceneTime)
        {
            loadingScene = true; // Mark loading scene
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        // Load the next scene
        SceneManager.LoadScene("Game");
        if (!Camera.main.enabled)
        {
            Camera.main.enabled = true;
        }
        if (!canvas.enabled)
        {
            canvas.enabled = true;
        }
    }
}
