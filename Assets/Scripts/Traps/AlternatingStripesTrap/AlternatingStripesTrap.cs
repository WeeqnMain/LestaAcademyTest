using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traps.AlternatingStripTrap
{
	public class AlternatingStripesTrap : MonoBehaviour
	{
		[SerializeField] private List<Stripe> stripes;

		[SerializeField] private float stripesShowTime;
		[SerializeField] private float switchCooldown;

		private readonly List<Stripe> oddStripes = new();
		private readonly List<Stripe> evenStripes = new();

		private void Start()
		{
			for (int i = 0; i < stripes.Count; i++)
			{
				stripes[i].Hide();
				if (i % 2 == 0)
					oddStripes.Add(stripes[i]);
				else
					evenStripes.Add(stripes[i]);
			}

			StartCoroutine(AlternateStripesRoutine());
		}

		private IEnumerator AlternateStripesRoutine()
		{
			ShowOddStripes();
			yield return new WaitForSeconds(switchCooldown);
			HideEvenStripes();
			yield return new WaitForSeconds(stripesShowTime);
			
			ShowEvenStripes();
			yield return new WaitForSeconds(switchCooldown);
			HideOddStripes();
			yield return new WaitForSeconds(stripesShowTime);
			
			StartCoroutine(AlternateStripesRoutine());
		}
	
		private void ShowOddStripes()
		{
			foreach (var stripe in oddStripes)
			{
				stripe.Show();
			}
		}
		
		private void HideOddStripes()
		{
			foreach (var stripe in oddStripes)
			{
				stripe.Hide();
			}
		}
		
		private void ShowEvenStripes()
		{
			foreach (var stripe in evenStripes)
			{
				stripe.Show();
			}
		}
		
		private void HideEvenStripes()
		{
			foreach (var stripe in evenStripes)
			{
				stripe.Hide();
			}
		}
	}
}
