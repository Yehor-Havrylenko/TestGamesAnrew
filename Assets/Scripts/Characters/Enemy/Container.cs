using System;
using Characters.Behaviors;
using Characters.Behaviors.Enemies;
using Characters.Stats;
using Pattern;
using Pools;
using UnityEngine;

namespace Characters.Enemy
{
    public class Container : MonoBehaviour, IAttackable, IPoolable<Container, EnemyType>
    {
        [SerializeField] private GameObject visualize;
        [SerializeField] private Health health;
        [SerializeField] private float maxHealth;
        [SerializeField] private int pointsForDeath;
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private Collider[] colliders;
        [SerializeField] private AbstractBehaviour behaviour;
        [SerializeField] private AnimationController animationController;
        [SerializeField] private AnimationEventsHandler animationEventsHandler;
        public event Action<Container, EnemyType> ReturnInPoolEvent;
        public CharacterType CharacterType => CharacterType.Enemy;
        public Vector3 Position => transform.position;
        public event Action<int> DeathEvent;
        private bool _isReturned;
        private Observer _stop;
        private Observer _continueGame;
        public bool IsReturned => _isReturned;
        
        private void OnValidate()
        {
            if (enemyType == EnemyType.Blue)
            {
                if (behaviour == null && !gameObject.TryGetComponent(out Blue blue))
                    gameObject.AddComponent<Blue>();
            }
            else if (enemyType == EnemyType.Red)
            {
                if (behaviour == null && !gameObject.TryGetComponent(out Red red))
                    gameObject.AddComponent<Red>();
            }

        }

        private void Awake()
        {
            animationEventsHandler.DeathEvent += Death;
            health.DeathEvent += behaviour.Death;
            behaviour.StopEvent += ReturnInPool;
        }

        public void SetPlayer(Player.Container value)
        {
            DeathEvent += value.AddScore;
            behaviour.SetPlayer(value);
            behaviour.SetAnimationController(animationController);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void InitObservers(Observer stop, Observer continueGame)
        {
            _stop = stop;
            _continueGame = continueGame;
            behaviour.InitObservers(_stop, _continueGame);
            visualize.SetActive(false);
        }
        public void Play()
        {
            visualize.SetActive(true);
            health.SetMaxHealth(maxHealth);
            foreach (var collider in colliders)
            {
                collider.enabled = true;
            }

            behaviour.Play();
            _isReturned = false;
        }

        public void Continue()
        {
            behaviour.Play();
        }

        public void TakeDamage(float value)
        {
            health.Spend(value);
        }

        public void ReturnInPool()
        {
            if(_isReturned) return;
            if (visualize != null) visualize.SetActive(false);
            ReturnInPoolEvent?.Invoke(this, enemyType);
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }

            behaviour.Stop();
            _isReturned = true;
        }

        public void Stop()
        {
            behaviour.Stop();
        }

        private void Death()
        {
            DeathEvent?.Invoke(pointsForDeath);
            ReturnInPool();
        }
    }
}