using System.Collections;
using Environment;
using TMPro;
using UnityEngine;

namespace UI
{
    public class RunTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private StartTrigger startTrigger;
    
        [field: SerializeField] public float Time { get; private set; }

        private Coroutine _timerRoutine;

        private void OnEnable()
        {
            startTrigger.PlayerExited += StartTimer;
            VictoryDefeatMediator.OnVictory += StopTimer;
            VictoryDefeatMediator.OnDefeat += StopTimer;
        }
    
        private void OnDisable()
        {
            startTrigger.PlayerExited -= StartTimer;
            VictoryDefeatMediator.OnVictory -= StopTimer;
            VictoryDefeatMediator.OnDefeat -= StopTimer;
        }

        private void StartTimer()
        {
            StopTimer();
            Time = 0f;
            _timerRoutine = StartCoroutine(TimerRoutine());
        }
    
        private void StopTimer()
        {
            StopAllCoroutines();
        }

        private IEnumerator TimerRoutine()
        {
            while (true)
            {
                Time += UnityEngine.Time.deltaTime;
                timerText.text = Time.ToString("0.00");
                yield return null;
            }
        }
    }
}
