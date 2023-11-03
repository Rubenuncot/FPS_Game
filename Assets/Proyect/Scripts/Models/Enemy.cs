namespace Proyect.Scripts.Models
{
    public class Enemy
    {
        private float _life;
        private float _speed;
        private float _damage;
        private static float _nr = -1;

        public Enemy()
        {
            Damage = 10;
            Life = 100;
            Speed = 10;
            GetEnemyNr();
            if (Nr % 10 == 0)
            {
                IncrementStats();
            }
        }

        private float Life
        {
            get => _life;
            set => _life = value;
        }

        private float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        private static float Nr
        {
            get => _nr;
            set => _nr = value;
        }

        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }

        private static void GetEnemyNr()
        {
            Nr++;
        }

        public float GetDamage(float damage)
        {
            Life -= damage;
            return Life;
        }

        private void IncrementStats()
        {
            Damage = (Damage + 5f) * (Nr * 0.5f);
            Speed = (Speed + 10) * (Nr * 0.5f);
            Life = (Life + 50) * (Nr * 0.5f);
        }

        public void BuffNerf(bool buff , float percent)
        {
            float finalPercent = (percent / 100);
            if (buff)
            {
                Damage *= 1 + finalPercent;
                Speed *= 1+ finalPercent;
                Life *= 1 + finalPercent;
            }
            else
            {
                Damage *= 1 - finalPercent;
                Speed *= 1 - finalPercent;
                Life *= 1 - finalPercent;
            }
        }
    }
}