using Characters.Behaviors;
using Characters.Behaviors.Controllers;
using Characters.Behaviors.Weapons;
using Characters.Player.Behaviors;
using Characters.Stats;
using Environment;
using Pattern;
using UnityEngine;

namespace Characters.Player
{
    public class Container : MonoBehaviour, IAttackable
    {
        [Header("Controllers")] [SerializeField]
        private PC pc;

        [SerializeField] private Mobile mobile;

        [Header("Stats")] [SerializeField] private Health health;
        [SerializeField] private Score score;
        [SerializeField] private Mana mana;

        [Space] 
        
        [SerializeField] private Movement movement;
        [SerializeField] private Rotation rotation;
        [SerializeField] private Pistol pistol;
        [SerializeField] private PositionTeleport positionTeleport;
        [SerializeField] private EnemyDetector enemyDetector;
        [SerializeField] private DamageUltaTrigger damageUltaTrigger;
        [SerializeField] private Observer restart;
        [SerializeField] private Observer stop;
        [SerializeField] private Observer continueGame;
        [SerializeField] private float maxHealth;
        [SerializeField] private float maxMana;
        private IObserverListenable _restartListenable;
        private Vector3 _startedPosition;
        private bool _ultaIsReady;
        CharacterType IAttackable.CharacterType => CharacterType.Player;
        public IHeal Heal => health;
        public IReplenishable Score => score;
        public IReplenishable Mana => mana;
        public Vector3 Position => transform.position;


        private void Awake()
        {
         /*   if (!Application.isMobilePlatform)
            {
                pc.DirectionMoveEvent += movement.Move;
                pc.DirectionRotateEvent += rotation.Rotate;
                pc.ShootEvent += pistol.Shoot;
                pc.UltaEvent += ActivateUlta;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (Application.isMobilePlatform)
            {
                mobile.MovementEvent += movement.Move;
                mobile.ShootEvent += pistol.Shoot;
                mobile.RotateAttackEvent += rotation.Rotate;
                mobile.RotateEvent += rotation.Rotate;
            }
            pistol.InitObservers(stop, continueGame);
            _startedPosition = transform.position;
            mana.ManaReadyEvent += () => _ultaIsReady = true;
            positionTeleport.SetPositionEvent += SetPosition;
            positionTeleport.SetEnemyDetector(enemyDetector);
            pistol.SetEnemyDetector(enemyDetector);
            pistol.SetUser(CharacterType.Player);
            _restartListenable = restart;
            _restartListenable.Subscribe(Play);
            Play();*/
        }

        private void Play()
        {
            health.SetMaxHealth(maxHealth);
            mana.SetMaxMana(maxMana);
            mana.Restore();
            score.Restore();
            transform.position = _startedPosition;
            _ultaIsReady = false;
        }
        public void AddScore(int value)
        {
            Mana.Replenish(value);
            Score.Replenish(value);
        }

        private void ActivateUlta()
        {
            _ultaIsReady = false;
            mana.Restore();
            damageUltaTrigger.ActivateUlta();
        }

        void IAttackable.TakeDamage(float value)
        {
            health.Spend(value);
        }

        private void SetPosition(Vector3 value) => transform.position = value;
    }
}