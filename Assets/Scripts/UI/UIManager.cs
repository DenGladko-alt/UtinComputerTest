using Utin;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameplayWindow;
    [SerializeField] private GameObject winWindow;
    [SerializeField] private GameObject loseWindow;

    [SerializeField] private PlayerBall playerBall;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        gameplayWindow.SetActive(newGameState == GameState.Gameplay);
        winWindow.SetActive(newGameState == GameState.Win);
        loseWindow.SetActive(newGameState == GameState.Lose);
    }
}
