using UnityEngine;

namespace Characters.Behaviors
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField] private float sensitivity;
        [SerializeField] private Transform capsule;
        private Transform _currentTransform;
        private float _rotationX;

        private void Awake()
        {
            _currentTransform = transform;
        }

        public void Rotate(Vector3 euler)
        {
            var mouseX = euler.x * sensitivity;

            capsule.Rotate(0, mouseX, 0);

            _rotationX -= euler.y * sensitivity;
            _rotationX = Mathf.Clamp(_rotationX, -70f, 70f);


             _currentTransform.localRotation = Quaternion.Euler(_rotationX,
                 _currentTransform.localRotation.eulerAngles.y, 0f);
        }
    }
}