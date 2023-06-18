using Events;
using Events.Implementations.Battle;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics
{
    public abstract class Battler : MonoBehaviour
    {
        [SerializeField] protected MeshRenderer meshRenderer;
        [SerializeField] protected Image healthBar;

        [HideInInspector] public string Name;
        
        private float m_power;
        private float m_health;

        private float m_maxHealth;

        public void SetReady(string _name, Color color, float health, float power)
        {
            gameObject.SetActive(true);

            Name = _name;
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
            
            if (this is Hero)
                GameEvents.Invoke<HeroIsShotEvent>();
            else
                GameEvents.Invoke<EnemyIsShotEvent>();

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
