using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Joystick
{
    public abstract class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] protected Image backGround;
        [SerializeField] protected Image center;
        [SerializeField] protected Color active;
        [SerializeField] protected Color inActive;
        [SerializeField] protected float sensitivity;
        protected Vector2 _inputVector;
        private Vector2 _bgSizeDelta;
        public event Action DragEvent;
        public event Action PointerDownEvent;
        public event Action PointerUpEvent;
        public event Action<Vector3> DirectionEvent;

        private bool _isActive = false;
        private void Start()
        {
            ClickEffect();
            _bgSizeDelta = backGround.rectTransform.sizeDelta;
        }
        private void Update()
        {
            Vector3 dir = new Vector3(_inputVector.x, 0, _inputVector.y);
            DirectionEvent?.Invoke(dir);
        }
        public virtual void OnDrag(PointerEventData eventData)
        {
            DragEvent?.Invoke();
            Vector2 position;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backGround.rectTransform, eventData.position,
                    null, out position))
            {
                position.x = (position.x * 2 / _bgSizeDelta.x);
                position.y = (position.y * 2 / _bgSizeDelta.y);

                _inputVector = new Vector2(position.x, position.y) * sensitivity; 
                _inputVector = (_inputVector.magnitude > 1f) ? _inputVector.normalized : _inputVector;

                center.rectTransform.anchoredPosition = new Vector2(_inputVector.x *
                    _bgSizeDelta.x / 2, _inputVector.y *
                    _bgSizeDelta.y / 2);
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            ClickEffect();
            PointerDownEvent?.Invoke();
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            ClickEffect();
            _inputVector = Vector3.zero;
            center.rectTransform.anchoredPosition = Vector2.zero;
            PointerUpEvent?.Invoke();
        }

        private void ClickEffect()
        {
            if (!_isActive)
            {
                center.color = active;
                _isActive = true;
            }
            else
            {
                center.color = inActive;
                _isActive = false;
            }
        }
    }
}
