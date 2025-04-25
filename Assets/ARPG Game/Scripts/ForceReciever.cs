using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReciever : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
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
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }
}
