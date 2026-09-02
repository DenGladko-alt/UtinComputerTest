using System;
using UnityEngine;

namespace Utin
{
    public class GameManager : MonoBehaviour
    {
        public static event Action<GameState> OnGameStateChanged;

        [SerializeField] private PlayerBall playerBall;

        public static GameState CurrentGameState { get; private set; } = GameState.Gameplay;

        private void Start()
        {
            ChangeGameState(GameState.Gameplay);
        }

        private void OnEnable()
        {
            RegisterEvents();
        }

        private void OnDisable()
        {
            UnregisterEvents();
        }

        private void RegisterEvents()
        {
            playerBall.OnBallReachedMinSize += OnBallReachedMinScale;
            FinishTrigger.OnFinishTriggered += OnFinishTriggered;
        }

        private void UnregisterEvents()
        {
            playerBall.OnBallReachedMinSize -= OnBallReachedMinScale;
            FinishTrigger.OnFinishTriggered -= OnFinishTriggered;
        }

        private void OnBallReachedMinScale()
        {
            ChangeGameState(GameState.Lose);
        }

        private void OnFinishTriggered()
        {
            ChangeGameState(GameState.Win);
        }

        private void ChangeGameState(GameState newGameState)
        {
            CurrentGameState = newGameState;
            OnGameStateChanged?.Invoke(CurrentGameState);
        }
    }

    public enum GameState
    {
        Gameplay = 1,
        Win = 2,
        Lose = 3
    }
}
