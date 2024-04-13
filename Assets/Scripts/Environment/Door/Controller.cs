using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Environment.Door
{
    public class Controller : MonoBehaviour
    {
        [Header("door opening direction")] [SerializeField]
        private RotateTo rotateTo;

        [Header("the point from which the door will rotate")] [SerializeField]
        private RotateFrom rotateFrom;

        [SerializeField] private Ease ease;
        [SerializeField] private List<Transform> points;
        [SerializeField] private Transform door;
        [SerializeField] private float duration;

        [Header("When animation ended return in start position")] [SerializeField]
        private bool isReset;

        [SerializeField] private bool isAutomatic;
        [SerializeField] private float time;
        private Transform _currentPoint;
        private Vector3[] _rotateValues = { new(0, -90, 0), new(0, 90, 0), new(90, 0, 0), new(-90, 0, 0) };
        private Coroutine _waitCoroutine;
        private bool _isBusy;

        private void Awake()
        {
            _currentPoint = points[(int)rotateFrom];
            door.SetParent(_currentPoint);
        }

        public void OnPlay()
        {
            if (_isBusy) return;
            _isBusy = true;
            Debug.Log("Play Door");
            _currentPoint.DOLocalRotate(_rotateValues[(int)rotateTo], duration)
                .SetEase(ease)
                .OnComplete(CheckConditions);
        }

        private void CheckConditions()
        {
            if (isAutomatic)
            {
                _waitCoroutine = StartCoroutine(WaitForResetTime());
            }
        }

        private IEnumerator WaitForResetTime()
        {
            yield return new WaitForSeconds(time);
            OnReset();
        }

        public void OnReset()
        {
            if (isReset && _isBusy)
            {
                Debug.Log("Reset Door");
                _currentPoint.DOLocalRotate(Vector3.zero, duration).SetEase(ease);
                _isBusy = false;
            }
        }

        private void OnDestroy()
        {
            if (_waitCoroutine != null) StopCoroutine(_waitCoroutine);
        }
    }
}