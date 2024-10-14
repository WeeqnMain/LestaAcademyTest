using System.Collections.Generic;
using UnityEngine;

namespace Traps.SlidingBarrierTrap
{
    public class SlidingBarrierTrap : MonoBehaviour
    {
        [SerializeField] private List<Barrier> barriers;

        private void Awake()
        {
            foreach (Barrier barrier in barriers)
            {
                barrier.Activate();
            }
        }
    }
}
