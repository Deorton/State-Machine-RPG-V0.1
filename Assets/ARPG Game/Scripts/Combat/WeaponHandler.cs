using System.Collections;
using System.Collections.Generic;
using ARPGGame.Combat;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Collider weaponCollider;
    [SerializeField] private Weapon currentWeapon;

    private float damage; // Default damage value
    private float Knockback;
    private float stunTime; // Default stun time value
    
    public void setCurrentWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        weaponCollider = weapon.GetComponent<Collider>();
        if (weaponCollider == null)
        {
            Debug.LogError("Weapon collider is not set.");
        }
    }

    public void EnableWeaponCollider()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
            currentWeapon.ClearList(); // Clear the list of collided objects when enabling the collider
        }
        else
        {
            Debug.LogError("Weapon collider is not set.");
        }
    }
    
    public void DisableWeaponCollider()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
        else
        {
            Debug.LogError("Weapon collider is not set.");
        }
    }

    public void SetAttackDamage(float BaseDamage, float knockback)
    {
        damage = BaseDamage; // Set the damage value from the AttackData component
        Knockback = knockback; // Set the knockback value from the AttackData component
    }

    public void SetStunTime(float stunTime)
    {
        this.stunTime = stunTime; // Set the stun time value from the AttackData component
    }

    public float GetDamage()
    {
        return damage; // Return the damage value
    }  

    public float GetKnockback()
    {
        return Knockback; // Return the damage value
    }  

    public float GetStunTime()
    {
        return stunTime; // Return the stun time value
    }
}
