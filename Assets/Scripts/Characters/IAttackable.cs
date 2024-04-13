using UnityEngine;

namespace Characters
{
    public interface IAttackable
    {
        public CharacterType CharacterType { get; }
        public Vector3 Position { get; }
        public void TakeDamage(float value);
    }
}