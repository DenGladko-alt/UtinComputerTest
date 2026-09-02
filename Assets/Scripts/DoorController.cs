using System;
using UnityEngine;

namespace Utin
{
    [RequireComponent(typeof(Animator))]
    public class DoorController : MonoBehaviour
    {
        public static event Action OnDoorOpened;
        
        private static readonly int OpenTrigger = Animator.StringToHash("OpenTrigger");
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.PLAYER_TAG))
            {
                OnDoorOpened?.Invoke();
                Open();
            }
        }

        private void Open()
        {
            _animator.SetTrigger(OpenTrigger);
        }
    }
}
