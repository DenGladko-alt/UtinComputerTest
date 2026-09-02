using System;
using System.Collections;
using UnityEngine;

namespace Utin
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        private const string OBSTACLE_TAG = "Obstacle";
        
        public static event Action OnProjectileExploded;
        
        [SerializeField] private float scaleMultiplier = 2f;
        [SerializeField] private float explosionSizeMultiplier = 1f;
        [SerializeField] private float launchSpeed = 100f;
        [SerializeField] private float cooldownTime = 2.5f;

        [SerializeField] private LayerMask obstacleLayer; 
        
        private Transform _transform;
        private GameObject _gameObject;
        private Rigidbody _rigidbody;
        
        public bool IsReadyForLaunch { get; private set; } = true;
        
        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            _gameObject = gameObject;
            
            ResetProjectile();
        }
        
        private void OnCollisionEnter(Collision other)
        {
            Explode();
        }
        
        private void ResetProjectile()
        {
            _rigidbody.linearVelocity = Vector3.zero;
            _transform.localScale = new Vector3(0f, 0f, 0f);
            _gameObject.SetActive(true);
        }

        public void IncreaseScale(float increaseScaleValue)
        {
            increaseScaleValue *= scaleMultiplier;
            _transform.localScale += new Vector3(increaseScaleValue, increaseScaleValue, increaseScaleValue);
        }
        
        public void Launch()
        {
            _rigidbody.linearVelocity = Vector3.forward * launchSpeed;
            IsReadyForLaunch = false;
        }

        private void Explode()
        {
            float explosionRadius = _transform.localScale.x * explosionSizeMultiplier;
            
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, obstacleLayer);

            foreach (Collider obstacleCollider in colliders)
            {
                obstacleCollider.GetComponent<IObstacle>()?.Explode();
            }
            
            ResetProjectile();

            StartCoroutine(CooldownProjectile());
            
            OnProjectileExploded?.Invoke();
        }

        public void SetPosition(Vector3 newPosition)
        {
            _transform.position = newPosition;
        }
        
        private IEnumerator CooldownProjectile()
        {
            yield return new WaitForSeconds(cooldownTime);

            IsReadyForLaunch = true;
        }
    }
}