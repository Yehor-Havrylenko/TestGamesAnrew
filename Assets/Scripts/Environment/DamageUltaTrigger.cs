using System.Collections.Generic;
using Characters;
using Pools;
using UnityEngine;

namespace Environment
{
    public class DamageUltaTrigger : MonoBehaviour
    {
        [SerializeField] private int enemyCount;
        private List<IBasePoolable> _allEnemies = new();

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                if (other.TryGetComponent(out IBasePoolable poolable))
                {
                    if (attackable.CharacterType != CharacterType.Player)
                    {
                        if (_allEnemies.Contains(poolable) == false)
                        {
                            _allEnemies.Add(poolable);
                            enemyCount = _allEnemies.Count;
                        }
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                if (other.TryGetComponent(out IBasePoolable poolable))
                {
                    if (attackable.CharacterType != CharacterType.Player)
                    {
                        if (_allEnemies.Contains(poolable))
                        {
                            _allEnemies.Remove(poolable);
                            enemyCount = _allEnemies.Count;
                        }
                    }
                }
            }
        }
        
        public void ActivateUlta()
        {
            foreach (var enemy in _allEnemies)
            {
                if (enemy != null && enemy.IsReturned == false) enemy.ReturnInPool();
            }
            _allEnemies.Clear();
        }
    }
}