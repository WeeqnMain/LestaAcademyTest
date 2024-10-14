using System;
using System.Collections;
using System.Collections.Generic;
using PlayerComponents;
using UnityEngine;

namespace Traps.WindTrap
{
    public class WindTrap : MonoBehaviour
    {
        public event Action<Vector3> WindDirectionChanged;
    
        [SerializeField] private TrapTrigger trapTrigger;
    
        [SerializeField] private float windForce;
        [SerializeField] private float changeDirectionCooldown;
    
        private Vector3 _windDirection;

        private readonly List<PlayerMovement> _playersToApplyForce = new();
    
        private System.Random _random;

        private void Awake()
        {
            _random = new System.Random();
        
            trapTrigger.TrapEnteredByPlayer += player => OnPlayerEntered(player.MovementComponent);
            trapTrigger.TrapExitedByPlayer += player => OnPlayerExited(player.MovementComponent);
        
            StartCoroutine(WindChangeDirectionRoutine());
        }

        private void FixedUpdate()
        {
            if (_playersToApplyForce.Count > 0)
            {
                ApplyForceToPlayers();
            }
        }

        private IEnumerator WindChangeDirectionRoutine()
        {
            var windAngle = _random.Next(0, 360);
            var direction = Quaternion.Euler(0, windAngle, 0) * Vector3.forward;
            _windDirection = direction;
        
            WindDirectionChanged?.Invoke(_windDirection);
        
            yield return new WaitForSeconds(changeDirectionCooldown);
            StartCoroutine(WindChangeDirectionRoutine());
        }
    
        private void OnPlayerEntered(PlayerMovement playerMovement)
        {
            _playersToApplyForce.Add(playerMovement);
        }
	
        private void OnPlayerExited(PlayerMovement playerMovement)
        {
            _playersToApplyForce.Remove(playerMovement);
        }
    
        private void ApplyForceToPlayers()
        {
            foreach (var player in _playersToApplyForce)
            {
                player.ApplyForce(_windDirection * (windForce * Time.fixedDeltaTime));
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + _windDirection);
        }
    }
}
