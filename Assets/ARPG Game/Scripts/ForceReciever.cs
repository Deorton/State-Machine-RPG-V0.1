using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReciever : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private float ImpactSmoothTime;

    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;
    public Vector3 movement => impact + Vector3.up * verticalVelocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("CharacterController not found on the GameObject.");
        }
    }

    void Update()
    {
        if(verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, ImpactSmoothTime);

        if(impact.sqrMagnitude < 0.2f * 0.2f)
        {
            impact = Vector3.zero; // Reset impact if it's very small

            if (navAgent != null)
            {
                navAgent.enabled = true; // Re-enable NavMeshAgent when impact is zero
            }
        }
    }

    public void AddForce(Vector3 force)
    {
        impact += force;

        if (navAgent != null)
        {
            navAgent.enabled = false; // Disable NavMeshAgent when applying force
        }
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }

    public void Reset()
    {
        impact = Vector3.zero;
        verticalVelocity = 0f;

    //    if (navAgent != null)
    //    {
    //        navAgent.enabled = true; // Re-enable NavMeshAgent when resetting
    //    }
    }
}
