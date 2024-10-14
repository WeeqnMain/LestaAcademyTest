using UnityEngine;

namespace UI
{
	public class WinScreen : MonoBehaviour
	{
		[SerializeField] private GameObject _winScreen;
		
		private void OnEnable()
		{
			VictoryDefeatMediator.OnVictory += Show;
			Hide();
		}
		
		private void OnDisable()
		{
			VictoryDefeatMediator.OnVictory -= Show;
		}

		private void Show()
		{
			Cursor.lockState = CursorLockMode.None;
			_winScreen.SetActive(true);
		}
		
		private void Hide()
		{
			_winScreen.SetActive(false);
		}
	}
}