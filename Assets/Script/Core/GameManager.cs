using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameState CurrentGameState { get; private set; } = GameState.MAINMENU;
    public static Action onGameStart;
    public static void GameStart() => onGameStart?.Invoke();

    public void StartGame()
    {
        GameStart();
        SetGameState(GameState.PLAYING);
    }

    public static void SetGameState(GameState targetState)
    {
        CurrentGameState = targetState;
    }
}

public enum GameState
{
    MAINMENU,
    PLAYING,
    ENDGAME
}