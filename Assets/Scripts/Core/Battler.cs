using UnityEngine;

namespace Core
{
    public abstract class Battler : MonoBehaviour
    {
        public float Health;
        public float AttackPower;

        public void GetDamage(float damage)
        {
            Health -= damage;

            if (Health > 0f)
                return;
            
            Health = 0f;
            Die();
        }

        public void Die()
        {
            //
        }
    }
}