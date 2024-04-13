using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters.Behaviors;
using Environment;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters.Player.Behaviors
{
    public class PositionTeleport : MonoBehaviour
    {
        [SerializeField] private RandomPointGenerator pointGenerator;
        [SerializeField] private List<CollisionDetect> walls;
        private Vector3 _currentPosition;
        private EnemyDetector _enemyDetector;
        public event Action<Vector3> SetPositionEvent;
        private void Awake()
        {
            foreach (var wall in walls)
            {
                //wall.CollisionEvent += Teleport;
            }
        }

        public void SetEnemyDetector(EnemyDetector value) => _enemyDetector = value;
        private void Teleport()
        {
            StartCoroutine(CheckPosition());
        }

        private IEnumerator CheckPosition()
        {
            List<Vector3> _clearPoints = new List<Vector3>();
            List<float> _pointIsEnemy = new List<float>();
            var points = pointGenerator.Init();
            foreach (var point in points)
            {
                var completed = false;
                _enemyDetector.SetPosition(point);
                _enemyDetector.Init();
                _enemyDetector.CompleteEvent += () => completed = true;
                yield return new WaitUntil(() => completed == true);
                if (_enemyDetector.HasEnemies)
                {
                    _pointIsEnemy.Add(_enemyDetector.MaxDistanceEnemy);
                }
                else
                {
                    _clearPoints.Add(point);
                }
            }

            if (_clearPoints.Count > 0)
            {
                var positionIndex = Random.Range(0, _clearPoints.Count);
                _currentPosition = _clearPoints[positionIndex];
            }
            else
            {
                var maxDistance = _pointIsEnemy.Max();
                _currentPosition = points[_pointIsEnemy.FindIndex((value) => maxDistance == value)];
            }

            SetPositionEvent?.Invoke(_currentPosition);
        }
    }
}