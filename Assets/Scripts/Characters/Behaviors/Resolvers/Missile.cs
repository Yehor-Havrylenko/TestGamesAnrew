using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Behaviors.Resolvers
{
    public class Missile : HitResolver
    {
        private List<Projectile> _actualRockets;
        private IAttackable _player;

        private void Awake()
        {
            _actualRockets = new List<Projectile>();
        }

        public void SetPlayer(IAttackable value) => _player = value;

        public override void SetBullet(Projectile projectile)
        {
            base.SetBullet(projectile);
            _actualRockets.Add(projectile);
        }

        public void RemoveBullet(Projectile value)
        {
            if (_actualRockets.Contains(value))
            {
                _actualRockets.Remove(value);
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.K)) _actualRockets.Clear();
            foreach (var rocket in _actualRockets)
            {
                rocket.Fly(_player.Position);
            }
        }

        protected override IEnumerator CheckAndHandle(Projectile projectile)
        {
            yield break;
        }
    }
}