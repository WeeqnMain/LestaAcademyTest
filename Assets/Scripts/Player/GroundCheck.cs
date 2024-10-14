using System;
using System.Collections.Generic;
using Traps;
using UnityEngine;

namespace PlayerComponents
{
    public class GroundCheck : MonoBehaviour
    {
        public event Action StartedFalling;
        public event Action EnteredGround;
        public event Action ExitedGround;
        
        [SerializeField] private int groundLayerNumber = 3;
        [SerializeField] private LayerMask groundLayerMask;

        [SerializeField] private float distanceToGroundToFall;
        
        public bool IsOnGround { get; private set; }
        
        private readonly List<Collider> _groundColliders = new();

        private bool _isFalling;

        private void Awake()
        {
            IsOnGround = true;
        }

        private void FixedUpdate()
        {
            CheckIfFalling();
        }

        private void CheckIfFalling()
        {
            if (IsOnGround)
            {
                _isFalling = false;
                return;
            }

            if (_isFalling) return;
            
            if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, distanceToGroundToFall, groundLayerMask))
            {
                _isFalling = true;
                StartedFalling?.Invoke();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == groundLayerNumber)
            {
                _groundColliders.Add(other);
                EnteredGround?.Invoke();
                IsOnGround = true;

                if (other.TryGetComponent(out IDeactivatableTrap deactivatableTrap))
                {
                    deactivatableTrap.Deactivated += OnTriggerExit;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_groundColliders.Contains(other) == false) return;
            
            if (other.TryGetComponent(out IDeactivatableTrap deactivatableTrap))
            {
                deactivatableTrap.Deactivated -= OnTriggerExit;
            }
            
            if (other.gameObject.layer == groundLayerNumber)
            {
                _groundColliders.Remove(other);
                if (_groundColliders.Count == 0)
                {
                    ExitedGround?.Invoke();
                    IsOnGround = false;
                }
            }
        }
    }
}
