using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Mechanics
{
    public abstract class Battler : MonoBehaviour
    {
        [SerializeField] protected MeshRenderer meshRenderer;
        [SerializeField] protected Image healthBar;
        
        protected float health;
        protected float power;

        private float m_maxHealth;

        public void SetReady(Color color, float _health, float _power)
        {
            gameObject.SetActive(true);
            
            meshRenderer.material.color = color;
            m_maxHealth = _health;
            health = _health;
            power = _power;

            UpdateHealthBar();
        }
        
        public void GetDamage(float damage)
        {
            health -= damage;
            UpdateHealthBar();
            
            if (health > 0f)
                return;
            
            health = 0f;
            Die();
        }

        private void UpdateHealthBar()
        {
            healthBar.fillAmount = health / m_maxHealth;
        }
        
        private void Die()
        {
            //
        }
    }
}
