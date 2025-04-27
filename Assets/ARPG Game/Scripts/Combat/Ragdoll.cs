using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;

    private Rigidbody[] allRigidbodies;
    private Collider[] allColliders;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        allRigidbodies = GetComponentsInChildren<Rigidbody>(true);
        allColliders = GetComponentsInChildren<Collider>(true);

        ToggleRagdoll(false); // Start with ragdoll disabled
    }

    public void ToggleRagdoll(bool isRagdoll)
    {
        foreach(Collider collider in allColliders)
        {
            if (collider.gameObject.CompareTag("Ragdoll")) // Exclude the CharacterController collider
            {
                collider.enabled = isRagdoll;
            }
        }

        foreach(Rigidbody rigidbody in allRigidbodies)
        {
            if (rigidbody.gameObject.CompareTag("Ragdoll")) // Exclude the CharacterController collider
            {
                rigidbody.isKinematic = !isRagdoll;
                rigidbody.useGravity = isRagdoll;
            }
        }

        animator.enabled = !isRagdoll; // Disable the animator when ragdoll is enabled
        characterController.enabled = !isRagdoll; // Disable the CharacterController when ragdoll is enabled
    }
}
