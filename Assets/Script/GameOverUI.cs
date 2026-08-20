using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject parentPanel;
    [SerializeField] private TextMeshProUGUI endCauseTxt;
    //[SerializeField] private GameState isPlayFlag = GameState.MainMenu;

    private void OnEnable()
    {
        GameManager.instance.onGameEnd += RenderPanel;
        //GameManager.instance.onGameState += OnValGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.instance.onGameEnd -= RenderPanel;
        //GameManager.instance.onGameState -= OnValGameStateChange;
    }

    private void RenderPanel(GameEndCause cause)
    {
        parentPanel.SetActive(true);

        if (endCauseTxt != null)
        {
            switch (cause)
            {
                case GameEndCause.Obstacle:
                    endCauseTxt.text = "Game over by obstacle";
                    break;
                case GameEndCause.Border:
                    endCauseTxt.text = "Game over by border";
                    break;
                default:
                    break;
            }
        }
    }

    public void ResetBtn()
    {
        GameManager.instance.SetGameState(GameState.MainMenu);
        GameManager.instance.ResetToMainMenu();
        parentPanel.SetActive(false);
    }

    //private void OnValGameStateChange(GameState newState)
    //{
    //    isPlayFlag = newState;
    //}
}
