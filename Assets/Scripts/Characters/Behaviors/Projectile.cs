using Pattern;
using System;
using System.Collections;
using UnityEngine;

namespace Characters.Behaviors
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected Rigidbody rb;
        [SerializeField] protected GameObject visualize;
        protected float _flySpeed;
        private float _lifeTime;
        private float _previousSpeed;
        private float _damage;
        private CharacterType _whoIsUse;
        private IObserverListenable _stopListenable;
        private IObserverListenable _continueListenable;
        public event Action<Projectile> ReturnInPoolEvent;
        public event Action<Projectile> OnCollision;
        private Coroutine _lifeTimer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 3) ReturnInPool();
            if (other.TryGetComponent(out IAttackable attackable))
            {
                if(attackable.CharacterType == _whoIsUse)return;
                attackable.TakeDamage(_damage);
                OnCollision?.Invoke(this);
                ReturnInPool();
            }
        }
        public abstract void Fly(Vector3 value);

        public void SetUser(CharacterType value) => _whoIsUse = value;
        public void SetFlySpeed(float value) => _flySpeed = value;

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetRotation(Quaternion quaternion)
        {
            transform.rotation = quaternion;
        }
        public void SetDamage(float value)
        {
            _damage = value;
        }

        public void Finish()
        {
            visualize.SetActive(false);
            rb.velocity = Vector3.zero;
            if(_lifeTimer!=null) StopCoroutine(_lifeTimer);
        }

        private void Stop()
        {
            _previousSpeed = _flySpeed;
            rb.velocity = Vector3.zero;
            _flySpeed = 0;
            if (_lifeTimer != null) StopCoroutine(_lifeTimer); 
        }

        private void Continue()
        {            
            _flySpeed = _previousSpeed;
            Fly(transform.forward);
            _lifeTimer = StartCoroutine(StartLifeTimer(_lifeTime));
        }

        public virtual void Init()
        {
            visualize.SetActive(true);
            _lifeTimer = StartCoroutine(StartLifeTimer(_lifeTime));
        }
        public void InitObservers(IObserverListenable stop, IObserverListenable continueGame)
        {
            _stopListenable = stop;
            _continueListenable = continueGame;
            _stopListenable.Subscribe(Stop);
            _continueListenable.Subscribe(Continue);
        }

        public void SetLifeTime(float value) => _lifeTime = value;
        private void ReturnInPool()
        {
            if(_lifeTimer!=null) StopCoroutine(_lifeTimer);
            ReturnInPoolEvent?.Invoke(this);
        }

        private IEnumerator StartLifeTimer(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            ReturnInPoolEvent?.Invoke(this);
        }
    }
}