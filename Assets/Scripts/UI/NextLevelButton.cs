using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    public void NextLevel()
    {
        var currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        PlayerPrefs.SetInt("CurrentLevel", currentLevel + 1);
        SceneManager.LoadScene(0);
    }
}
