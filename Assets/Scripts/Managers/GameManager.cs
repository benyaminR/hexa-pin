using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState gameState = GameState.Idle;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;
    }

    public void ChangeGameState(GameState gameState)
    {
        this.gameState = gameState;
        EventManager.Instance.Invoke("OnGameStateChange", gameState);
    }
}

public enum GameState
{
    Idle,
    Playing,
    Finished
}
