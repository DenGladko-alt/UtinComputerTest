using System;
using UnityEngine;

namespace Utin
{
    public class PlayerBall : MonoBehaviour
    {
        // Events
        public event Action<float> OnBallSizeChanged; 
        public event Action OnBallReachedMinSize;
        
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float checkDistance = 2f;
        [SerializeField] private LayerMask obstacleLayer;
        
        [Header("Scale")]
        [SerializeField] private float minScale = 1f;
        [SerializeField] private float scalingSpeed = 1f;
        [SerializeField] private float initialShotScale = 0.25f;

        [Header("Shooting")]
        [SerializeField] private Projectile projectile;
        [SerializeField] private Vector3 projectileOffset;
        
        [SerializeField] private bool debug = false;
        
        // Cashed values
        private Transform _transform;
        private float _maxScale;

        public bool CanMove { get; private set; } = false;

        private void Awake()
        {
            _transform = transform;
            _maxScale = transform.localScale.x;
        }
        
        void FixedUpdate()
        {
            if (!CanMove) return;
            
            if (GameManager.CurrentGameState == GameState.Lose || GameManager.CurrentGameState == GameState.Win) return;
            
            if (Physics.SphereCast(_transform.position, _transform.localScale.x / 2f, _transform.forward,
                    out RaycastHit hit, checkDistance, obstacleLayer))
            {
                CanMove = false;
                return;
            }
            
            _transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
        }
        
        // For debug
        void OnDrawGizmos()
        {
            if (debug == false) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.forward * checkDistance, transform.localScale.x / 2f);
        }

        private void OnEnable()
        {
            Obstacle.OnObstacleExploded += OnObstacleExploded;
        }

        private void OnDisable()
        {
            Obstacle.OnObstacleExploded -= OnObstacleExploded;
        }
        
        private void OnObstacleExploded()
        {
            CanMove = true;
        }
        
        public void PrepareProjectile()
        {
            projectile.SetPosition(_transform.position + projectileOffset);
            UpdateScales(initialShotScale, false);
        }
        
        public void UpdateScales(float scaleReduction, bool useScalingSpeed)
        {
            if (useScalingSpeed)
            {
                scaleReduction *= scalingSpeed;
            }
            
            float newScale  = Mathf.Clamp(_transform.localScale.x - scaleReduction, minScale, _maxScale);
            _transform.localScale = new Vector3(newScale, newScale, newScale);
            
            projectile.IncreaseScale(scaleReduction);
            
            OnBallSizeChanged?.Invoke(transform.localScale.x/_maxScale);
            
            if (_transform.localScale.x <= minScale)
            {
                OnBallReachedMinSize?.Invoke();
            }
        }

        public void LaunchProjectile()
        {
            projectile.Launch();
        }
    }
}