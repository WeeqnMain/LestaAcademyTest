using System.Collections;
using UnityEngine;

namespace PlayerComponents
{
    public class PlayerVisuals : MonoBehaviour
    {
        #region AnimatorFields

        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Fall = Animator.StringToHash("Fall");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");
        private static readonly int IsOnGround = Animator.StringToHash("IsOnGround");
    
        #endregion

        [SerializeField] private Animator animator;
    
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private GroundCheck groundCheck;

        [SerializeField] private float distanceToGroundToFall;

        private Coroutine _setSpeedRoutine;
        
        private void OnEnable()
        {
            playerMovement.Jumped += OnJump;
            playerMovement.Moved += OnMove;
            groundCheck.EnteredGround += OnGrounded;
            groundCheck.StartedFalling += OnFall;
        }

        private void OnDisable()
        {
            playerMovement.Jumped -= OnJump;
            playerMovement.Moved -= OnMove;
            groundCheck.EnteredGround -= OnGrounded;
            groundCheck.StartedFalling -= OnFall;
            
            animator.SetBool(IsMoving, false);
        }

        private void Update()
        {
            animator.SetBool(IsMoving, playerMovement.IsMoving);
        }

        private void OnMove(float speedRatio)
        {
            SetAnimatorSpeed(speedRatio);
        }

        private void OnFall()
        {
            animator.SetBool(IsOnGround, false);
            animator.SetTrigger(Fall);
        }
        
        private void OnJump()
        {
            animator.SetBool(IsOnGround, false);
            animator.SetTrigger(Jump);
        }

        private void OnGrounded()
        {
            animator.SetBool(IsOnGround, true);
        }

        private void SetAnimatorSpeed(float speed)
        {
            if (_setSpeedRoutine != null)
                StopCoroutine(_setSpeedRoutine);
            _setSpeedRoutine = StartCoroutine(SetAnimatorSpeedRoutine(speed));
        }
    
        private IEnumerator SetAnimatorSpeedRoutine(float speed)
        {
            var oldSpeed = animator.GetFloat(MovementSpeed);
            var progress = 0f;
            while (progress < 1f)
            {
                progress += Time.deltaTime * 2f;
                animator.SetFloat(MovementSpeed, Mathf.Lerp(oldSpeed, speed, progress));
                yield return null;
            }
        }
    }
}
