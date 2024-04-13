using UnityEngine;

namespace Characters.Behaviors.Weapons
{
    public class Missile : Weapon<Resolvers.Missile>
    {
        public void SetPlayer(IAttackable value) => resolver.SetPlayer(value);
        protected override void AddInstance()
        {
            var instance = Instantiate(projectilePrefab, center.position + Vector3.forward, center.rotation);
            instance.ReturnInPoolEvent += ReturnBullet;
            instance.ReturnInPoolEvent += resolver.RemoveBullet;
            instance.SetFlySpeed(bulletSpeed);
            instance.SetDamage(damage);
            instance.InitObservers(_stop, _continue);
            _instances.Enqueue(instance);
        }
    }
}