using System.Collections;
using Characters.Behaviors;
using Characters.Enemy;
using Pattern;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Pools.Enemy enemyPool;
        [SerializeField] private float timeToSpawn = 1f;
        [SerializeField] private RandomPointGenerator pointGenerator;
       
        [Header("Observers")] 
        [SerializeField] private Observer restart;
        [SerializeField] private Observer death;
        [SerializeField] private Observer stop;
        [SerializeField] private Observer continueGame;
        private IObserverListenable _restartListenable;
        private IObserverListenable _deathListenable;
        private IObserverListenable _stopListenable;
        private IObserverListenable _continueListenable;

        private Coroutine _spawnCoroutine;
        private int _countEnemiesAlive;
        private bool _isStop;

        private void Awake()
        {
            _restartListenable = restart;
            _deathListenable = death;
            _stopListenable = stop;
            _continueListenable = continueGame;
            _deathListenable.Subscribe(SpawnStop);
            _stopListenable.Subscribe(SpawnStop);
            _restartListenable.Subscribe(SpawnStart);
            _continueListenable.Subscribe(SpawnStart);
            _spawnCoroutine = StartCoroutine(Spawn());
            enemyPool.ReturnedEnemyEvent += DecrementEnemy;
        }

        private void SpawnStart()
        {
            _spawnCoroutine = StartCoroutine(Spawn());
        }

        private void SpawnStop()
        {
            if (_spawnCoroutine != null) StopCoroutine(_spawnCoroutine);
        }

       

        private IEnumerator Spawn()
        {
            for (;;)
            {
                var spawnsPos = pointGenerator.Init();
                var currentPoint = Random.Range(0, spawnsPos.Count);
                var randomEnemy = Random.Range(0, 2);
                EnemyType enemyType = randomEnemy == 0 ? EnemyType.Blue : EnemyType.Red;
                yield return new WaitForSeconds(timeToSpawn);
                if (_countEnemiesAlive >= 30) continue;
                timeToSpawn = Mathf.Clamp(timeToSpawn - 2, 1, 10);

                if (_isStop) continue;
                var position = spawnsPos[currentPoint];
                var enemy = enemyPool.GetInPool(enemyType);
                enemy.SetPosition(position);
                enemy.Play();
                _countEnemiesAlive += 1;
            }
        }

        private void DecrementEnemy(EnemyType enemyType)
        {
            _countEnemiesAlive -= 1;
        }

        private void OnDestroy()
        {
            if (_spawnCoroutine != null) StopCoroutine(_spawnCoroutine);
            _deathListenable.Unsubscribe(SpawnStop);
            _restartListenable.Unsubscribe(SpawnStart);
        }
    }
}