using System;
using System.Collections.Generic;
using Characters.Enemy;
using Pattern;
using UnityEngine;

namespace Pools
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Container prefabRed;
        [SerializeField] private Container prefabBlue;
        [SerializeField] private int minCountBlue;
        [SerializeField] private int minCountRed;
        [SerializeField] private Characters.Player.Container player;
        [SerializeField] private UI.Play.Counter counter;
        [Header("Observers")] 

        [SerializeField] private Observer death;
        [SerializeField] private Observer restart;
        [SerializeField] private Observer stop;
        [SerializeField] private Observer continueGame;
        private IObserverListenable _restartListenable;
        private IObserverListenable _deathListenable;
        private IObserverListenable _stopListenable;
        private IObserverListenable _continueListenable;
        private Dictionary<EnemyType, Queue<IPoolable<Container, EnemyType>>> _instances;
        public event Action<EnemyType> ReturnedEnemyEvent;


        private void Awake()
        {
            _deathListenable = death;
            _restartListenable = restart;
            _stopListenable = stop;
            _continueListenable = continueGame;
            _instances = new Dictionary<EnemyType, Queue<IPoolable<Container, EnemyType>>>();
            _instances.Add(EnemyType.Blue, new Queue<IPoolable<Container, EnemyType>>());
            _instances.Add(EnemyType.Red, new Queue<IPoolable<Container, EnemyType>>());
            for (int i = 0; i < minCountBlue; i++)
            {
                AddInstance(EnemyType.Blue);
            }

            for (int i = 0; i < minCountRed; i++)
            {
                AddInstance(EnemyType.Red);
            }
        }

        private void AddInstance(EnemyType type)
        {
            var instance = Instantiate(type == EnemyType.Blue ? prefabBlue : prefabRed);
            instance.ReturnInPoolEvent += ReturnInPool;
            instance.DeathEvent += _ => counter.AddDiedEnemy();
            _deathListenable.Subscribe(instance.Stop);
            _restartListenable.Subscribe(instance.ReturnInPool);
            _stopListenable.Subscribe(instance.Stop);
            _continueListenable.Subscribe(instance.Continue);
            instance.InitObservers(stop, continueGame);
            instance.SetPlayer(player);
            _instances[type].Enqueue(instance);
        }

        private void ReturnInPool(Container value, EnemyType type)
        {
            value.SetPosition(new Vector3(0, -1, 0));
            ReturnedEnemyEvent?.Invoke(type);
            _instances[type].Enqueue(value);
        }

        public IPoolable<Container, EnemyType> GetInPool(EnemyType type)
        {
            if (_instances[type].Count == 0)
            {
                AddInstance(type);
            }

            return _instances[type].Dequeue();
        }
    }
}