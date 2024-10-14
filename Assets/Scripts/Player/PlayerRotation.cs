using UnityEngine;

namespace PlayerComponents
{
	public class PlayerRotation : MonoBehaviour
	{
		[SerializeField] private PlayerMovement playerMovement;
		[SerializeField] private GameObject characterModel;
	
		[SerializeField] private float rotationSpeed;
	
		private Coroutine _rotationCoroutine;

		private void Update()
		{
			if (playerMovement.MoveDirection != Vector3.zero)
				SetModelRotation(playerMovement.MoveDirection);
		}

		private void SetModelRotation(Vector3 moveDirection)
		{
			var rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
			characterModel.transform.rotation = Quaternion.RotateTowards(characterModel.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
		}
	}
}