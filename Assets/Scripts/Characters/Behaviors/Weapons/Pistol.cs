using Environment;

namespace Characters.Behaviors.Weapons
{
    public class Pistol : Weapon<Resolvers.Pistol>
    {
        public void SetEnemyDetector(EnemyDetector value) => resolver.SetEnemyDetector(value);
    }
}