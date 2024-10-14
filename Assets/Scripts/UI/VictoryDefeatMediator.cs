using System;
using PlayerComponents;
using UnityEngine;

namespace UI
{
	public class VictoryDefeatMediator : MonoBehaviour
	{
		[SerializeField] private PlayerHealth playerHealth;
		
		public static event Action OnVictory;
		public static event Action OnDefeat;

		private void OnEnable()
		{
			playerHealth.Dead += TriggerDefeat;
		}
		
		private void OnDisable()
		{
			playerHealth.Dead -= TriggerDefeat;
		}

		public static void TriggerDefeat()
		{
			OnDefeat?.Invoke();
		}

		public static void TriggerVictory()
		{
			OnVictory?.Invoke();
		}
	}
}