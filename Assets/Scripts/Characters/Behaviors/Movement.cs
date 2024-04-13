using UnityEngine;

namespace Characters.Behaviors
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float speed;

        public void Move(Vector3 direction)
        {
            Vector3 moveDirectionForward = transform.forward * direction.z;
            Vector3 moveDirectionSide = transform.right * direction.x;

            Vector3 dir = (moveDirectionForward + moveDirectionSide).normalized;
            characterController.Move(dir * Time.deltaTime * speed);
        }
    }
}