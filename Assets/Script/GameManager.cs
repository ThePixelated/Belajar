using System;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameState gameState = GameState.MainMenu;
    public GameState GameState { get { return gameState; } private set { gameState = value; } }

    public Action<GameEndCause> onGameEnd;
    public Action<GameState> onGameState;
    public Action<Sensitivity> onSensitivity;
    public Action onResetToMainMenu;

    private void Awake()
    {
        instance = this;
    }

    public void GameEnd(GameEndCause cause) => onGameEnd?.Invoke(cause);
    //public void curGameState(GameState state) => onGameState?.Invoke(state);
    public void ConfigSensitivity(Sensitivity sens) => onSensitivity?.Invoke(sens);
    public void ResetToMainMenu() => onResetToMainMenu?.Invoke();


    public void MainMenuLevel() => SetGameState(GameState.MainMenu);
    public void StartLevel() => SetGameState(GameState.Playing);
    public void EndLevel() => SetGameState(GameState.EndGame);

    public void SetGameState(GameState state)
    {
        GameState = state;
        onGameState?.Invoke(state);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}

public enum GameEndCause
{
    Obstacle,
    Border
}

public enum GameState
{
    MainMenu,
    Playing,
    EndGame
}