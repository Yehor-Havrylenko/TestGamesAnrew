using Pattern;
using System;
using UnityEngine;

namespace Characters.Behaviors.Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class AbstractBehaviour : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float moveSpeed;

        protected Player.Container _player;
        protected bool _isStarted;
        protected bool _isStoped;
        private Vector3 _previousVelocity;
        protected IObserverListenable _stopListenable;
        protected IObserverListenable _continueListenable;
        protected AnimationController _animationContoller;
        public event Action StopEvent;
        public virtual void SetPlayer(Player.Container value) => _player = value;
        public virtual void InitObservers(IObserverListenable stop, IObserverListenable continueGame)
        {
            _stopListenable = stop;
            _continueListenable = continueGame;
        }

        public virtual void Play()
        {
            _isStarted = true;
            _animationContoller.IdleBool(false);
            rb.detectCollisions = true;
        }

        public virtual void Stop()
        {
            _isStarted = false;
            _animationContoller.IdleBool(true);
        }

        protected virtual void Update()
        {
            if (!_isStarted && !_isStoped)
            {
                _previousVelocity = rb.velocity;
                rb.velocity = Vector3.zero;
                rb.freezeRotation = true;
                _isStoped = true;
            }
            else if(_isStarted && _isStoped)
            {
                rb.velocity = _previousVelocity;
                rb.freezeRotation = false;
                _isStoped = false;
            }
        }
        protected void Move(Vector3 inputMove, Vector3 lookDirection)
        {
            // move
            rb.velocity = inputMove * moveSpeed;

            // turn
            lookDirection.y = 0; // in same plane
            if (lookDirection != Vector3.zero) rb.MoveRotation(Quaternion.LookRotation(lookDirection, Vector3.up));
        }

        protected void StopEventCallback()
        {
            StopEvent?.Invoke();
        }

        public void SetAnimationController(AnimationController value)
        {
            _animationContoller = value;
        }

        public void Death()
        {
            _isStarted = false;
            rb.detectCollisions = false;
            _animationContoller.SetBodyLayerWeight(1);
            _animationContoller.SetLegsLayerWeight(0);
            _animationContoller.DeathTrigger();
        }
    }
}