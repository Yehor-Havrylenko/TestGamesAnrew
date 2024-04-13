using UnityEngine;

namespace Characters.Behaviors.Projectiles
{
    public class Bullet : Projectile
    {
        public override void Fly(Vector3 value)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(value * Time.deltaTime * _flySpeed, ForceMode.Impulse); 
        }
    }
}