using System;
using PlayerComponents;
using UnityEngine;

namespace Environment
{
	public class StartTrigger : MonoBehaviour
	{
		public event Action PlayerExited;
		
		private void OnTriggerExit(Collider other)
		{
			if (other.GetComponent<Player>())
			{
				PlayerExited?.Invoke();
			}
		}
	}
}