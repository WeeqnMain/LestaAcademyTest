using System;
using UnityEngine;
using PlayerComponents;

namespace Traps
{
    public class TrapTrigger : MonoBehaviour
    {
        public event Action<Player> TrapEnteredByPlayer;
        public event Action<Player> TrapExitedByPlayer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                TrapEnteredByPlayer?.Invoke(player);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                TrapExitedByPlayer?.Invoke(player);
            }
        }
    }
}