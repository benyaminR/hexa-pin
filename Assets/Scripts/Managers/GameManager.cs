using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{

    public GameState gameState = GameState.Idle;

    private void Awake()
    {
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
    Finished,
    Win,
    Lose
}
