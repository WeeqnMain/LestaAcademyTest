using System;
using UnityEngine;

namespace PlayerComponents
{
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField] private float runMoveSpeed;
		[SerializeField] private float walkMoveSpeed;
		[SerializeField] private float jumpForce;
	
		[SerializeField] private GroundCheck groundCheck;
		[SerializeField] private LayerMask wallLayer;

		private Vector2 _moveDirection;
		private float _currentMoveSpeed;
		private bool _jumpPending;
	
		private Rigidbody _rigidbody;
		private PlayerInput _playerInput;
		private Transform _cameraTransform;
		private Vector3 _forceToApply;

		public bool IsMoving => _moveDirection != Vector2.zero;

		public event Action Jumped;
		public event Action<float> Moved;
		public Vector3 MoveDirection => GetMoveDirection();

		private void Awake()
		{
			_currentMoveSpeed = runMoveSpeed;
		
			_rigidbody = GetComponent<Rigidbody>();
			_cameraTransform = Camera.main.transform;
		
			_playerInput = GetComponent<PlayerInput>();
		}

		private void OnEnable()
		{
			_playerInput.JumpPerformed += OnJumpPerformed;
			_playerInput.WalkMovementStarted += SetSpeedToWalk;
			_playerInput.WalkMovementEnded += SetSpeedToRun;
		}

		private void OnDisable()
		{
			_playerInput.JumpPerformed -= OnJumpPerformed;
			_playerInput.WalkMovementStarted -= SetSpeedToWalk;
			_playerInput.WalkMovementEnded -= SetSpeedToRun;
		}

		private void Update()
		{
			_moveDirection = _playerInput.GetMoveDirection();
		}

		private void FixedUpdate()
		{
			if (_moveDirection != Vector2.zero)
			{
				Move();
			}
			
			_rigidbody.AddForce(_forceToApply, ForceMode.Impulse);
			_forceToApply = Vector3.zero;

			if (_jumpPending)
			{
				if (groundCheck.IsOnGround)
					Jump();
				_jumpPending = false;
			}
		}

		public void ApplyForce(Vector3 force)
		{
			_forceToApply += force;
		}

		private void Move()
		{
			var movement = GetMoveDirection();
		
			var point1 = transform.position + Vector3.down * 0.17f;
			var point2 = transform.position + Vector3.up * 0.175f;
		
			var desiredDirection = movement * (_currentMoveSpeed * Time.fixedDeltaTime);

			if (Physics.CapsuleCast(point1, point2, 0.18f, movement, out RaycastHit hit, 0.05f, wallLayer))
			{
				float angle = Vector3.Angle(movement, hit.normal);
				float speedScale = (180f - angle) / 90f;

				Vector3 slideDirection = Vector3.ProjectOnPlane(movement, hit.normal).normalized;

				desiredDirection = slideDirection * (_currentMoveSpeed * Time.fixedDeltaTime * speedScale);
			}
			
			_rigidbody.MovePosition(_rigidbody.position + desiredDirection);
			
			Moved?.Invoke(GetSpeedRatio());
		}
	
		private void Jump()
		{
			_rigidbody.AddForce(jumpForce * Vector3.up);
		
			Jumped?.Invoke();
		}

		private void OnJumpPerformed()
		{
			_jumpPending = true;
		}

		private Vector3 GetMoveDirection()
		{
			return Quaternion.LookRotation(GetCameraForwardDirection()) * new Vector3(_moveDirection.x, 0, _moveDirection.y);
		}

		private Vector3 GetCameraForwardDirection()
		{
			var direction = _cameraTransform.forward;
			direction.y = 0;
			return direction;
		}

		private void SetSpeedToWalk()
		{
			_currentMoveSpeed = walkMoveSpeed;
		}

		private void SetSpeedToRun()
		{
			_currentMoveSpeed = runMoveSpeed;
		}

		private float GetSpeedRatio() => _currentMoveSpeed == walkMoveSpeed ? 0f : 1f;
	}
}