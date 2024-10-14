using UnityEngine;

namespace PlayerComponents
{
	[RequireComponent(typeof(PlayerInput))]
	public class CameraLookPointRotation : MonoBehaviour
	{
		[SerializeField] private bool inverseVerticalRotation;
		
		[SerializeField] private Transform lookPoint;
		[SerializeField] private float verticalRotationPower;
		[SerializeField] private float horizontalRotationPower;
		[SerializeField] private float maxVerticalAngle;
		[SerializeField] private float minVerticalAngle;
    
		private PlayerInput _playerInput;
	
		private void Awake()
		{
			Cursor.lockState = CursorLockMode.Locked;
			_playerInput = GetComponent<PlayerInput>();
		}
	
		private void Update()
		{
			var rotationDelta = _playerInput.GetMouseDelta();
			if (inverseVerticalRotation)
				rotationDelta.y = -rotationDelta.y;
			
			lookPoint.rotation *= Quaternion.AngleAxis(rotationDelta.x * horizontalRotationPower * Time.deltaTime, Vector3.up);
			lookPoint.rotation *= Quaternion.AngleAxis(rotationDelta.y * horizontalRotationPower * Time.deltaTime, Vector3.right);
		
			var angles = lookPoint.localEulerAngles;
			angles.z = 0f;
			if (angles.x > 0 && angles.x < 180f)
			{
				angles.x = Mathf.Min(angles.x, maxVerticalAngle);
			}
			else if (angles.x < 360f && angles.x > 180f)
			{
				angles.x = Mathf.Max(angles.x, 360f + minVerticalAngle);
			} 
		
			lookPoint.localEulerAngles = angles;
		}
	}
}