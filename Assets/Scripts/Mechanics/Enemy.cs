using UnityEngine;

namespace Mechanics
{
    public class Enemy : Battler
    {
        public void SetReady()
        {
            gameObject.SetActive(true);
            
            power = 3f;
            health = 10f;

            healthBar.fillAmount = 1f;
        }
    }
}
