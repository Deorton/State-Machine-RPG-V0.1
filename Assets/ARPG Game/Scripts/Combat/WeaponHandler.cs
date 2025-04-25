using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Collider weaponCollider;

    public void SetWeaponCollider(Collider fromWeapon)
    {
        weaponCollider = fromWeapon;
    }

    public void EnableWeaponCollider()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
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
}
