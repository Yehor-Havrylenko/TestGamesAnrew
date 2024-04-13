using UnityEngine;

namespace Characters.Behaviors.Projectiles
{
    public class Rocket : Projectile
    {
        [SerializeField] private ParticleSystem particleSystem;
        private Vector3 _playerPos;
        private bool _isNotFoundPlayer;

        public override void Fly(Vector3 value)
        {
            _isNotFoundPlayer = false;
            _playerPos = value;
        }

        public override void Init()
        {
            base.Init();
            particleSystem.Play();
        }

        private void Move(Vector3 inputMove, Vector3 lookDirection)
        {
            rb.velocity = inputMove * _flySpeed;

            lookDirection.y = 0;
            rb.MoveRotation(Quaternion.LookRotation(lookDirection, Vector3.up));
        }

        private void Update()
        {
            var move = Vector3.zero;
            if (Vector3.Distance(_playerPos, transform.position) < 0.5 && _isNotFoundPlayer == false)
            {
                Move(transform.forward * _flySpeed, transform.forward);
                _isNotFoundPlayer = true;
            }
            else if (_isNotFoundPlayer == false)
            {
                var dirToPlayer = _playerPos - transform.position;
                dirToPlayer = dirToPlayer.normalized;
                Move(dirToPlayer, dirToPlayer);
            }
        }
    }
}