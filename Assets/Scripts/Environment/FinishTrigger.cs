using PlayerComponents;
using UI;
using UnityEngine;

namespace Environment
{
    public class FinishTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                VictoryDefeatMediator.TriggerVictory();
            }
        }
    }
}
