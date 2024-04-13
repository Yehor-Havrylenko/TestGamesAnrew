using System.Collections;
using Environment;
using Environment;
using Pattern;
using UnityEngine;

namespace Characters.Behaviors.Resolvers
{
    public class Pistol : HitResolver
    {
        [SerializeField] private Observer stop;
        [SerializeField] private Player.Container player;
        [SerializeField] private float highHealthChance = 0.2f;
        [SerializeField] private float mediumHealthChance = 0.4f;
        [SerializeField] private float lowMediumHealthChance = 0.6f;
        [SerializeField] private float lowHealthChance = 0.8f;
        private float _currentChance;
        private EnemyDetector _enemyDetector;

        private void Start()
        {
            player.Heal.UpdateEvent += SetChance;
        }
        public void SetEnemyDetector(EnemyDetector value) => _enemyDetector = value;

        private void SetChance(float value, float max)
        {
            if (value > max * 0.8)
            {
                _currentChance = highHealthChance;
            }
            else if (value > max * 0.5)
            {
                _currentChance = mediumHealthChance;
            }
            else if (value > max * 0.25)
            {
                _currentChance = lowMediumHealthChance;
            }
            else
            {
                _currentChance = lowHealthChance;
            }
        }

        protected override IEnumerator CheckAndHandle(Projectile projectile)
        {
            var completed = false;
            float randomValue = Random.value;

            var bulletPosition = projectile.transform.position;
            _enemyDetector.CompleteEvent += () => completed = true;
            _enemyDetector.Init();
            _enemyDetector.SetPosition(bulletPosition);
            yield return new WaitUntil(() => completed == true);
            if (_enemyDetector.HasEnemies && randomValue < _currentChance)
            {
                var enemyPosition = _enemyDetector.AllEnemies[_enemyDetector.MinIndexDistanceEnemy].Position;
                projectile.SetPosition(bulletPosition);
                var heading = enemyPosition - bulletPosition;
                var distance = heading.magnitude;
                if (distance <= 0.1f) yield break;
                var direction = heading / distance;
                projectile.Fly(direction);
                projectile.Init();
            }
        }
    }
}