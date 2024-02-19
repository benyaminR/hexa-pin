using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;


    public void Awake()
    {
        EventManager.Instance.Listen<GameState>("OnGameStateChange", OnGameStateChange);
    }

    private void OnGameStateChange(GameState state)
    {
        if (state == GameState.Win)
        {
            winPanel.SetActive(true);

        }
        if (state == GameState.Lose)
        {
            losePanel.SetActive(true);
        }
    }
}
