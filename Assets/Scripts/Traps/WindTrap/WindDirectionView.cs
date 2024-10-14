using UnityEngine;

namespace Traps.WindTrap
{
	public class WindDirectionView : MonoBehaviour
	{
		[SerializeField] private global::Traps.WindTrap.WindTrap windTrap;

		private void Awake()
		{
			windTrap.WindDirectionChanged += OnWindDirectionChanged;
		}

		private void OnWindDirectionChanged(Vector3 windDirection)
		{
			transform.LookAt(transform.position + windDirection);
		}
	}
}