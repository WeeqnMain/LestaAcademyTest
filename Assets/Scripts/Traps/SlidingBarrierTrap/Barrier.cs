using System.Collections;
using PlayerComponents;
using UnityEngine;

namespace Traps.SlidingBarrierTrap
{
    public class Barrier : MonoBehaviour
    {
        [SerializeField] private Vector3 extendOffset;
        [SerializeField] private float extendTime;
        [SerializeField] private float retractionTime;
        [SerializeField] private float waitTimeExtended;
        [SerializeField] private float waitTimeRetracted; 
        [SerializeField] private float startDelay;
    
        private Vector3 _startPosition;
        private Vector3 _extendPosition;
    
        private Coroutine _lifeRoutine;

        private void Awake()
        {
            _startPosition = transform.position;
            _extendPosition = transform.position + extendOffset;

            StartCoroutine(BarrierLifeRoutine());
        }

        public void Activate()
        {
            _lifeRoutine ??= StartCoroutine(BarrierLifeRoutine());
        }

        public void Deactivate()
        {
            StopAllCoroutines();
            _lifeRoutine = null;
        }
    
        private IEnumerator BarrierLifeRoutine()
        {
            yield return new WaitForSeconds(startDelay);

            while (true)
            {
                yield return StartCoroutine(MoveBarrierRoutine(_startPosition, _extendPosition, extendTime));
            
                yield return new WaitForSeconds(waitTimeExtended);
            
                yield return StartCoroutine(MoveBarrierRoutine(_extendPosition, _startPosition, retractionTime));
            
                yield return new WaitForSeconds(waitTimeRetracted);
            }
        }

        private IEnumerator MoveBarrierRoutine(Vector3 startPosition, Vector3 endPosition, float duration)
        {
            float time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);
                yield return null;
            } 
            transform.position = endPosition;
        }
    }
}