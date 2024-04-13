using System;
using UnityEngine;

namespace UI.Touches
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float rotateScreen;
        private Vector3 _position;
        private Vector2 _startedTouchPos;
        public event Action<Vector3> DirectionEvent;

        void Awake()
        {
            rotateScreen = Screen.width / 2.0f;
            _position = new Vector3(0.0f, 0.0f, 0.0f);
        }

        void Update()
        {
            foreach (var touch in Input.touches)
            {
                if (touch.position.x >= rotateScreen)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        _startedTouchPos = touch.position;
                    }

                    if (touch.phase == TouchPhase.Moved)
                    {
                        var pos = touch.position;
                        pos.x = (pos.x - _startedTouchPos.x);
                        pos.y = (pos.y - _startedTouchPos.y);
                        _position = new Vector3(pos.x, pos.y, 0.0f);
                        DirectionEvent?.Invoke(_position * Time.deltaTime);
                    }
                }
            }
        }
    }
}