using System.Collections;
using UnityEngine;

namespace Characters.Behaviors
{
    public abstract class HitResolver : MonoBehaviour
    {
        public virtual void SetBullet(Projectile projectile)
        {
            projectile.OnCollision += StartCheckAndHandle;
        }

        protected virtual void StartCheckAndHandle(Projectile projectile)
        {
            StartCoroutine(CheckAndHandle(projectile));
        }

        protected abstract IEnumerator CheckAndHandle(Projectile projectile);
    }
}