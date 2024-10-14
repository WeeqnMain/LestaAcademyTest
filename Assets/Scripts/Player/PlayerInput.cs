using System;
using UnityEngine;

namespace PlayerComponents
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerInputActions _playerInputActions;

        public event Action JumpPerformed;
        public event Action WalkMovementStarted;
        public event Action WalkMovementEnded;
    
        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
        
            _playerInputActions.Player.Jump.performed += _ => JumpPerformed?.Invoke();
            _playerInputActions.Player.Walk.started += _ => WalkMovementStarted?.Invoke();
            _playerInputActions.Player.Walk.canceled += _ => WalkMovementEnded?.Invoke();
        }

        public Vector2 GetMoveDirection()
        {
            return _playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
        }
    
        public Vector2 GetMouseDelta()
        {
            return _playerInputActions.Player.Look.ReadValue<Vector2>();
        }
    }
}
