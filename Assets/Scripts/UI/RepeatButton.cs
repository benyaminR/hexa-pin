using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RepeatButton : MonoBehaviour
{
    public void Repeat()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
