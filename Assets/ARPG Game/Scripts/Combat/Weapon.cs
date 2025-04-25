using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGGame.Combat
{
    /// <summary>
    /// This class is responsible for handling the weapon's properties and behaviors.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponHandler weaponHandler;

        private Collider weaponCollider;
        void Awake()
        {
            weaponHandler = GetComponentInParent<WeaponHandler>();

            if (weaponHandler == null)
            {
                print("WeaponHandler not found in the parent object.");
                Debug.LogError("WeaponHandler not found in the scene.");
            }

            weaponHandler.SetWeaponCollider(GetComponent<Collider>());
            weaponCollider = GetComponent<Collider>();
            weaponCollider.isTrigger = true; // Set the collider to be a trigger
            weaponCollider.enabled = false; // Disable the collider by default
        }

        
    }
}
