using System;
using UnityEngine;

namespace Traps
{
	public interface IDeactivatableTrap
	{
		public event Action<Collider> Deactivated;
	}
}