using Pattern;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Behaviors
{
    public abstract class Weapon<TResolver> : MonoBehaviour where TResolver : HitResolver
    {
        [SerializeField] protected Projectile projectilePrefab;
        [SerializeField] protected TResolver resolver;
        [SerializeField] protected Transform center;
        [SerializeField] protected float damage;
        [SerializeField] protected float bulletSpeed;
        [SerializeField] protected float bulletLifeTime;
        protected IObserverListenable _stop;
        protected IObserverListenable _continue;
        protected Queue<Projectile> _instances;
        private CharacterType _whoIsUse;

        protected virtual void Awake()
        {
            _instances = new Queue<Projectile>();
        }

        public void InitObservers(IObserverListenable stop, IObserverListenable continueGame)
        {
            _stop = stop;
            _continue = continueGame;
        }

        public virtual void Shoot()
        {
            if (_instances.Count <= 0) AddInstance();
            var instance = _instances.Dequeue();
            instance.SetPosition(center.position);
            instance.SetLifeTime(bulletLifeTime);
            instance.SetFlySpeed(bulletSpeed);
            instance.Fly(transform.forward);
            resolver.SetBullet(instance);
            instance.SetUser(_whoIsUse);
            instance.Init();
        }

        public void SetUser(CharacterType value) => _whoIsUse = value;

        protected virtual void AddInstance()
        {
            var instance = Instantiate(projectilePrefab, center.position + Vector3.forward, Quaternion.identity);
            instance.ReturnInPoolEvent += ReturnBullet;
            instance.SetDamage(damage);
            instance.InitObservers(_stop, _continue);
            _instances.Enqueue(instance);
        }

        protected virtual void ReturnBullet(Projectile value)
        {
            value.Finish();
            _instances.Enqueue(value);
        }
    }
}