using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics
{
    public abstract class Battler : MonoBehaviour
    {
        [SerializeField] protected MeshRenderer meshRenderer;
        [SerializeField] protected Image healthBar;
        
        private float m_power;
        private float m_health;

        private float m_maxHealth;

        public void SetReady(Color color, float health, float power)
        {
            gameObject.SetActive(true);
            
            meshRenderer.material.color = color;
            m_maxHealth = health;
            m_health = health;
            m_power = power;

            UpdateHealthBar();
        }

        public void Attack(Battler target)
        {
            Laser.Shoot(this, target, m_power);
        }
        
        public void GetDamage(float damage)
        {
            m_health -= damage;
            UpdateHealthBar();
            
            GameEvents.Invoke(this is Hero ? BattleEvent.HeroIsShot : BattleEvent.EnemyIsShot);
            
            if (m_health > 0f)
                return;
            
            m_health = 0f;
            Die();
        }

        private void UpdateHealthBar()
        {
            healthBar.fillAmount = m_health / m_maxHealth;
        }
        
        private void Die()
        {
            gameObject.SetActive(false);
            BattleManager.BattlerDown(this);
        }
    }
}
