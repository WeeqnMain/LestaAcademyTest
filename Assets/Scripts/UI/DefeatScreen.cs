using UnityEngine;

namespace UI
{
	public class DefeatScreen : MonoBehaviour
	{
		[SerializeField] private GameObject _defeatScreen;
		
		private void OnEnable()
		{
			VictoryDefeatMediator.OnDefeat += Show;
			Hide();
		}
		
		private void OnDisable()
		{
			VictoryDefeatMediator.OnDefeat -= Show;
			Hide();
		}

		private void Show()
		{
			Cursor.lockState = CursorLockMode.None;
			_defeatScreen.SetActive(true);
		}
		
		private void Hide()
		{
			_defeatScreen.SetActive(false);
		}
	}
}