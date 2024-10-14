using System.Collections;
using System.Collections.Generic;
using PlayerComponents;
using UnityEngine;

namespace Traps.DamageTrap
{
	public class DamageTrap : MonoBehaviour
	{
		[SerializeField] private TrapTrigger trapTrigger;
	
		[SerializeField] private int damage; 
		[SerializeField] private float activateDuration;
		[SerializeField] private float damageDuration;
		[SerializeField] private float cooldownDuration;
	
		[SerializeField] private Color activateColor;
		[SerializeField] private Color damageColor;
		[SerializeField] private Color cooldownColor;
		private Color _normalColor;

		private Material _trapMaterial;

		private bool _isActivated;

		private readonly List<PlayerHealth> _playersToDamage = new();
	
		private void Awake()
		{
			_trapMaterial = GetComponent<Renderer>().material;
			_normalColor = _trapMaterial.color;

			trapTrigger.TrapEnteredByPlayer += player => OnPlayerEntered(player.HealthComponent); 
			trapTrigger.TrapExitedByPlayer += player => OnPlayerExited(player.HealthComponent);
		}

		private void OnPlayerEntered(PlayerHealth playerHealth)
		{
			_playersToDamage.Add(playerHealth);
			if (_isActivated == false)
			{
				Activate();
			}
		}
	
		private void OnPlayerExited(PlayerHealth playerHealth)
		{
			_playersToDamage.Remove(playerHealth);
		}

		private void Activate()
		{
			StartCoroutine(ActivateRoutine());
		}

		private IEnumerator ActivateRoutine()
		{
			_isActivated = true;
		
			_trapMaterial.color = activateColor;
			yield return new WaitForSeconds(activateDuration);
		
			_trapMaterial.color = damageColor;
			DamagePlayersOnTrap();
			yield return new WaitForSeconds(damageDuration);
		
			_trapMaterial.color = cooldownColor;
			trapTrigger.gameObject.SetActive(false);
			yield return new WaitForSeconds(cooldownDuration);
		
			_trapMaterial.color = _normalColor;
			trapTrigger.gameObject.SetActive(true);
		
			_isActivated = false;
		}

		private void DamagePlayersOnTrap()
		{
			foreach (var player in _playersToDamage)
			{
				player.TakeDamage(damage);
			}
		}
	}
}
