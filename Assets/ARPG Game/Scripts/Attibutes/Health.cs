using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Attibutes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        public float currentHealth;
        private bool isInvulnerable = false;

        public event Action<float> OnTakeDamage;
        public event Action OnDie;

        public bool isDead = false;

        // Start is called before the first frame update
        void Start()
        {
            maxHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
            currentHealth = maxHealth;
        }

        public void SetInvulnerable(bool isInvulnerable)
        {
            this.isInvulnerable = isInvulnerable;
        }

        public void TakeDamage(float damage, float stunTime, GameObject attacker)
        {
            if (isDead) return;
            if (isInvulnerable) return;

            currentHealth = Mathf.Max(currentHealth - damage, 0);
            OnTakeDamage?.Invoke(stunTime);
            //    Debug.Log($"{gameObject.name} took {damage} damage. Current health: {currentHealth}");

            if (currentHealth == 0)
            {
                AwardExperience(attacker);
                
                isDead = true;
                OnDie?.Invoke();
            }
        }

        private void AwardExperience(GameObject attacker)
        {
            attacker.GetComponent<Experience>()?.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        private void Die()
        {
            isDead = true;
            // Add death logic here (e.g., play animation, disable character, etc.)
            Debug.Log($"{gameObject.name} has died.");
        }

        public float GetHealthPercentage()
        {
            return 100 * (currentHealth / maxHealth);
        }
    }
}
