using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;

    public GameObject levelCompletedPanel;
    public GameObject gameOverPanel;


    private void Awake()
    {
        instance = this;
    }


    public void LevelCompleted()
    {
        levelCompletedPanel.SetActive(true);
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
    public void OpenThisPanel(GameObject thisPanel)
    {
        thisPanel.SetActive(true);
    }
    public void CloseThisPanel(GameObject thisPanel)
    {
        thisPanel.SetActive(false);
    }
   
}
