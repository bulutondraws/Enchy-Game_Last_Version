using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReset : MonoBehaviour
{
    public void RestartGame(string thisLevel)
    {
        SceneManager.LoadScene(thisLevel);
    }
}
