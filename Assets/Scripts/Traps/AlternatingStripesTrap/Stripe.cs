using System;
using UnityEngine;

namespace Traps.AlternatingStripTrap
{
	public class Stripe : MonoBehaviour, IDeactivatableTrap
	{
		public event Action<Collider> Deactivated;
		
		private Renderer _renderer;
		private Collider _collider;

		private void Awake()
		{
			_renderer = GetComponent<Renderer>();
			_collider = GetComponent<Collider>();
		}

		public void Show()
		{
			_renderer.enabled = true;
			_collider.enabled = true;
		}

		public void Hide()
		{
			_renderer.enabled = false;
			_collider.enabled = false;
			
			Deactivated?.Invoke(_collider);
		}
	}
}