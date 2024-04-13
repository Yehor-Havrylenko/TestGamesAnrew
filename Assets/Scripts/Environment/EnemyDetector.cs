using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters;
using UnityEngine;

namespace Environment
{
    public class EnemyDetector : MonoBehaviour
    {
        private List<float> _distance;
        private List<IAttackable> _enemies;
        private bool _findEnemies;
        public bool HasEnemies => _findEnemies;
        public float MaxDistanceEnemy => _distance.Max();
        public float MinDistanceEnemy => _distance.Min();
        public int MaxIndexDistanceEnemy => _distance.FindIndex(x => x == MaxDistanceEnemy);
        public int MinIndexDistanceEnemy => _distance.FindIndex(x => x == MaxDistanceEnemy);
        public List<IAttackable> AllEnemies => new(_enemies);
        public event Action CompleteEvent;
        private bool _isActive;

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Init()
        {
            _distance = new List<float>();
            _enemies = new List<IAttackable>();
            _findEnemies = false;
            _isActive = true;
            StartCoroutine(WorkTimer());
        }

        private IEnumerator WorkTimer()
        {
            yield return new WaitForSeconds(0f);
            CompleteEvent?.Invoke();
            _isActive = false;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_isActive == false) return;
            if (other.TryGetComponent(out IAttackable attackable))
            {
                if (attackable.CharacterType == CharacterType.Enemy)
                {
                    if (_enemies.Contains(attackable) == false)
                    {
                        _distance.Add(Vector3.Distance(attackable.Position, transform.position));
                        _enemies.Add(attackable);
                    }

                    if (_findEnemies == false) _findEnemies = true;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isActive == false) return;
            if (other.TryGetComponent(out IAttackable attackable))
            {
                if (attackable.CharacterType == CharacterType.Enemy)
                {
                    if (_enemies.Contains(attackable) == false)
                    {
                        _distance.Add(Vector3.Distance(attackable.Position, transform.position));
                        _enemies.Add(attackable);
                    }

                    if (_findEnemies == false) _findEnemies = true;
                }
            }
        }
    }
}