using UI;
using UnityEngine;

namespace PlayerComponents
{
	[RequireComponent(typeof(PlayerHealth), typeof(PlayerMovement), typeof(CameraLookPointRotation))]
	public class Player : MonoBehaviour
	{
		public PlayerHealth HealthComponent { get; private set; }
		public PlayerMovement MovementComponent { get; private set; }
		public PlayerVisuals PlayerVisuals { get; private set; }
		public CameraLookPointRotation CameraLookPointRotation { get; private set; }

		private void Awake()
		{
			HealthComponent = GetComponent<PlayerHealth>();
			MovementComponent = GetComponent<PlayerMovement>();
			CameraLookPointRotation = GetComponent<CameraLookPointRotation>();
			PlayerVisuals = GetComponentInChildren<PlayerVisuals>();
		}

		private void OnEnable()
		{
			VictoryDefeatMediator.OnDefeat += Deactivate;
			VictoryDefeatMediator.OnVictory += Deactivate;
		}

		private void OnDisable()
		{
			VictoryDefeatMediator.OnDefeat -= Deactivate;
			VictoryDefeatMediator.OnVictory -= Deactivate;
		}

		private void Deactivate()
		{
			HealthComponent.enabled = false;
			MovementComponent.enabled = false;
			CameraLookPointRotation.enabled = false;
			PlayerVisuals.enabled = false;
		}
	}
}
