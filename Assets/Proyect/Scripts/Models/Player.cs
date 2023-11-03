using UnityEditorInternal;

namespace Proyect.Scripts.Models
{
    public class Player
    {
        private float _life;
        private float _speed;
        private float _damage;
        private bool _isDead;
        private bool _canHealth;
        private bool _canSpeedUp;
        private static int _nr;

        public Player()
        {
            _nr += 1;
            if (_nr > 0)
            {
                SetDefaultValues();
            }
        }
        
        public float Life
        {
            get => _life;
            set => _life = value;
        }

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }

        public bool IsDead
        {
            get => _isDead;
            set => _isDead = value;
        }

        public bool CanHealth
        {
            get => _canHealth;
            set => _canHealth = value;
        }

        public bool CanSpeedUp
        {
            get => _canSpeedUp;
            set => _canSpeedUp = value;
        }

        private void SetDefaultValues()
        {
            Life = 100;
            Speed = 10;
            Damage = 10;
            IsDead = false;
            CanHealth = true;
            CanSpeedUp = true;
        }

        public void TakeDamage(float damage)
        {
            Life -= damage;
            if (Life <= 0)
            {
                IsDead = true;
            }
        }

        /*
         * type is the stat that changes when the buff or nerf is applied
         */
        public void BuffNerf(bool buff, float percent, string type)
        {
            float finalPercent = percent / 100;
            if (buff)
            {
                switch (type)
                {
                    case "damage":
                        Damage *= 1 + finalPercent;
                        break;
                    case "speed":
                        Speed *= 1 + finalPercent;
                        CanSpeedUp = true;
                        break;
                    default:
                        Damage *= 1 + finalPercent;
                        Speed *= 1 + finalPercent;
                        CanSpeedUp = true;
                        CanHealth = true;
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case "damage":
                        Damage *= 1 - finalPercent;
                        break;
                    case "speed":
                        Speed *= 1 - finalPercent;
                        CanSpeedUp = true;
                        break;
                    default:
                        Damage *= 1 - finalPercent;
                        Speed *= 1 - finalPercent;
                        break;
                }
            }
        }

        public void Healing(float health)
        {
            if (!IsDead)
            {
                Life += health;
            }
        }
    }
}