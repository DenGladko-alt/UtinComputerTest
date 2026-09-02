using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Obstacle : MonoBehaviour, IObstacle
{
    private static readonly int TickingTrigger = Animator.StringToHash("Ticking");

    public static event Action OnObstacleExploded;
    
    [SerializeField] private float destroyDelay = 1f;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void Explode()
    {
        _animator.SetTrigger(TickingTrigger);

        StartCoroutine(DelayedExplosion());
    }

    private IEnumerator DelayedExplosion()
    {
        yield return new WaitForSeconds(destroyDelay);

        OnObstacleExploded?.Invoke();

        Destroy(gameObject);
    }
}