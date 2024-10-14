using PlayerComponents;
using UI;
using UnityEngine;

namespace Environment
{
    public class FallTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                VictoryDefeatMediator.TriggerDefeat();
            }
        }
    }
}
