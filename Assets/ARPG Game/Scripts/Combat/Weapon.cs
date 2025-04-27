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
        [SerializeField] private Collider myColiider;
        private Collider weaponCollider;
        public List<Collider> alReadyCollidedWith = new List<Collider>();
        private float damage; // Default damage value
        private float knockback; // Default knockback value
        private float stunTime; // Default stun time value

        void Awake()
        {
            weaponHandler = GetComponentInParent<WeaponHandler>();

            if (weaponHandler == null)
            {
                print("WeaponHandler not found in the parent object.");
                Debug.LogError("WeaponHandler not found in the scene.");
            }

            weaponCollider = GetComponent<Collider>();
            weaponHandler.setCurrentWeapon(this); // Set the current weapon in the WeaponHandler
            weaponCollider.isTrigger = true; // Set the collider to be a trigger
            weaponCollider.enabled = false; // Disable the collider by default

            myColiider = transform.root.GetComponent<CapsuleCollider>();
            alReadyCollidedWith.Add(myColiider); // Add the character controller to the list to avoid self-collision
        }

        void OnTriggerEnter(Collider other)
        {
            if(other == myColiider) return; // Ignore the trigger if it is the same as the weapon collider

            if(alReadyCollidedWith.Contains(other)) return; // Ignore if already collided with this object
            alReadyCollidedWith.Add(other); // Add the object to the list of collided objects

            if(other.TryGetComponent<Health>(out Health health)) // Check if the other object has a Health component
            {
                damage = weaponHandler.GetDamage(); // Get the base damage from the AttackData component
                stunTime = weaponHandler.GetStunTime(); // Get the stun time from the AttackData component
                health.TakeDamage(damage, stunTime); // Call the TakeDamage method on the Health component
            }

            if(other.TryGetComponent<ForceReciever>(out ForceReciever forceReciever)) // Check if the other object has a ForceReciever component
            {
                knockback = weaponHandler.GetKnockback(); // Get the knockback value from the AttackData component
                forceReciever.AddForce((other.transform.position - myColiider.transform.position).normalized * knockback);
            }
        }

        public void ClearList()
        {
            alReadyCollidedWith.Clear(); // Clear the list of collided objects
            alReadyCollidedWith.Add(myColiider); // Add the character controller to the list to avoid self-collision
        }
    }
}
