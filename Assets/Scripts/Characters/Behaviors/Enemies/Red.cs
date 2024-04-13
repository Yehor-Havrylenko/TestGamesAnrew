using System.Collections;
using Characters.Stats;
using UnityEngine;

namespace Characters.Behaviors.Enemies
{
    public class Red : AbstractBehaviour
    {
        [SerializeField] private float coolDown;
        [SerializeField] private float damage;
        [SerializeField] private CapsuleCollider capsuleCollider;

        private Coroutine _move;

        public override void Play()
        {
            base.Play();
            capsuleCollider.enabled = false;
            Move(Vector3.up, Vector3.up);
            _move = StartCoroutine(Fly());
        }

        public override void Stop()
        {
            base.Stop();
            if(_move!=null) StopCoroutine(_move);
        }

        private IEnumerator Fly()
        {
            yield return new WaitForSeconds(coolDown);
            var heading = _player.Position - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            Move(direction, direction);
            _animationContoller.AttackTrigger();
            capsuleCollider.enabled = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent(out IHeal player))
            {
                player.Spend(damage);
            }
            StopEventCallback();
        }
    }
}
