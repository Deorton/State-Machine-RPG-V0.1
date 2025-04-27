using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    public float currentHealth;

    public event Action<float> OnTakeDamage;
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage, float stunTime)
    {
        if (isDead) return;

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        OnTakeDamage?.Invoke(stunTime);
        Debug.Log($"{gameObject.name} took {damage} damage. Current health: {currentHealth}");

        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        // Add death logic here (e.g., play animation, disable character, etc.)
        Debug.Log($"{gameObject.name} has died.");
    }
}
