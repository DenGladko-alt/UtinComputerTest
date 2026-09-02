using System;
using UnityEngine;

namespace Utin
{
    public class FinishTrigger : MonoBehaviour
    {
        public static event Action OnFinishTriggered;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.PLAYER_TAG))
            {
                OnFinishTriggered?.Invoke();
            }
        }
    }
}
