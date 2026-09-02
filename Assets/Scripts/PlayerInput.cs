using UnityEngine;

namespace Utin
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private PlayerBall playerBall;
        [SerializeField] private Projectile projectile;

        private float _holdTime = 0f;
        private bool _isHolding = false;
        private bool _isProcessingInput = true;

        private Touch _currentTouch;

        private void OnEnable()
        {
            playerBall.OnBallReachedMinSize += OnBallReachedMinScale;
        }

        private void OnDisable()
        {
            playerBall.OnBallReachedMinSize -= OnBallReachedMinScale;
        }

        private void OnBallReachedMinScale()
        {
            _isProcessingInput = false;
        }

        private void Update()
        {
            if (!_isProcessingInput) return;

            if (!projectile.IsReadyForLaunch) return;

            if (GameManager.CurrentGameState != GameState.Gameplay) return;

            if (playerBall.CanMove) return;

            // Mouse button down - start holding
            if (Input.GetMouseButtonDown(0))
            {
                _isHolding = true;
                _holdTime = 0f;
                playerBall.PrepareProjectile();
            }
            // Mouse button held - continue holding
            else if (Input.GetMouseButton(0) && _isHolding)
            {
                _holdTime += Time.deltaTime;

                // Reduce ball scale while we hold
                playerBall.UpdateScales(Time.deltaTime, true);
            }
            // Mouse button up - release and launch
            else if (Input.GetMouseButtonUp(0) && _isHolding)
            {
                _isHolding = false;
                playerBall.LaunchProjectile();
            }
        }
    }
}
