using System.Collections;
using Characters.Behaviors.Weapons;
using Characters.Player;
using Pattern;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters.Behaviors.Enemies
{
    public class Blue : AbstractBehaviour
    {
        [SerializeField] private Missile missile;
        [SerializeField] private float shootDistance;
        [SerializeField] private float maxIntervalToChangeStrafeDirection = 5;
        [SerializeField] private float shootDelay;

        private float _strafeDir = 1;
        private float _distToPlayerRndCoeff = 1;
        private float _nextTimeToChangeStrafe;
        private Vector3 _prevPlayerPos;

        protected override void Update()
        {
            base.Update();
            if (!_isStarted) return;
            if (_player == null) return;
            Move();
        }
        

        public override void InitObservers(IObserverListenable stop, IObserverListenable continueGame)
        {
            base.InitObservers(stop, continueGame);
            missile.InitObservers(_stopListenable, _continueListenable);
        }
        public override void SetPlayer(Container value)
        {
            base.SetPlayer(value);
            missile.SetUser(CharacterType.Enemy);
            missile.SetPlayer(value);
            StartCoroutine(LoopedShoot());
        }

        private void Move()
        {
            var move = Vector3.zero;
            var playerPos = _player.transform.position;
            var distToPlayer = (playerPos - transform.position).magnitude;

            var dirToPlayer = (ForecastPosition(playerPos) - 
                               transform.position).normalized;
            
            move = CheckDistance(distToPlayer,dirToPlayer,move);
            
            move += Vector3.Cross(Vector3.up, dirToPlayer) * Strafe();

            move.Normalize();

            Move(move, dirToPlayer);
        }

        private Vector3 CheckDistance(float distToPlayer, Vector3 dirToPlayer,Vector3 move)
        {
            if (distToPlayer > shootDistance * _distToPlayerRndCoeff)
            { 
                move += dirToPlayer;
            }

            else if (distToPlayer < shootDistance * _distToPlayerRndCoeff * 0.9f)
            { 
                move -= dirToPlayer;
            }
            return move;
        }

        private Vector3 ForecastPosition(Vector3 playerPos)
        {
            var playerVelocity = (playerPos - _prevPlayerPos) / Time.deltaTime;
            _prevPlayerPos = playerPos;
            var playerPosForecast = playerPos + playerVelocity;
            return playerPosForecast;
        }

        private float Strafe()
        {
            if (Time.time >= _nextTimeToChangeStrafe)
            {
                _nextTimeToChangeStrafe = Time.time + Random.value * maxIntervalToChangeStrafeDirection;
                GenerateNextDistToPlayerCoeff();
                return _strafeDir *= -1;
            }
            return _strafeDir;
        }

        private void GenerateNextDistToPlayerCoeff()
        {
            _distToPlayerRndCoeff = Random.Range(0.1f, 0.95f);
        }

        private IEnumerator LoopedShoot()
        {
            for (; ; )
            {
                if (_isStarted) missile.Shoot();

                yield return new WaitForSeconds(shootDelay);
            }
        }

        public override void Play()
        {
            _animationContoller.SetBodyLayerWeight(1);
            _animationContoller.SetLegsLayerWeight(1);
            base.Play();
        }

        public override void Stop()
        {
            _animationContoller.SetBodyLayerWeight(1);
            _animationContoller.SetLegsLayerWeight(0);
            base.Stop();
        }
    }
}
