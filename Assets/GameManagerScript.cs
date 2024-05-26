using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI, victoryUI;
    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }
    public void Victory()
    {
        Time.timeScale = 0;
        victoryUI.SetActive(true);
    }
}
