using UnityEngine;

namespace Characters.Behaviors
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private static readonly int attackTrigger = Animator.StringToHash("Attack");
        private static readonly int attackBool = Animator.StringToHash("Attack");
        private static readonly int idleBool = Animator.StringToHash("Idle");
        private static readonly int deathTrigger = Animator.StringToHash("Death");

        public void AttackTrigger()
        {
            animator.SetTrigger(attackTrigger);
        }
        public void AttackBool(bool value)
        {
            animator.SetBool(attackBool, value);
        }

        public void IdleBool(bool value)
        {
            animator.SetBool(idleBool, value);
        }

        public void SetBodyLayerWeight(float value)
        {
            animator.SetLayerWeight(0, value);
        }

        public void SetLegsLayerWeight(float value)
        {
            animator.SetLayerWeight(1, value);
        }

        public void DeathTrigger()
        {
            animator.SetTrigger(deathTrigger);
        }
    }
}