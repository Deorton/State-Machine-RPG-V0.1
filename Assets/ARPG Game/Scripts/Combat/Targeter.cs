using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetGroup;
    [SerializeField] private Camera mainCamera;

    public Target CurrentTarget { get; private set; }
    public List<Target> targets = new List<Target>();

    void Awake()
    {
        cineTargetGroup = GameObject.FindWithTag("Player").GetComponentInChildren<CinemachineStateDrivenCamera>().GetComponentInChildren<CinemachineTargetGroup>();
        if (cineTargetGroup == null)
        {
            Debug.LogError("CinemachineTargetGroup not found in children.");
        }  

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }

        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }

        RemoveTarget(target);
    }

    public bool SelectTarget()
    {
        ClearTargetGroupList();

        if (targets.Count == 0) { return false; }

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach (Target target in targets)
        {
            if (target == null) { continue; }

            Vector2 screenPos = mainCamera.WorldToViewportPoint(target.transform.position);
            
            if(screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1)
            {
                continue;
            }

            Vector2 toCenter = screenPos - new Vector2(0.5f, 0.5f);

            if(toCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTargetDistance = toCenter.sqrMagnitude;
                closestTarget = target;
            }
        }

        if (closestTarget == null) { return false; }

        CurrentTarget = closestTarget;
        cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);

        return true;
    }

    public void Cancel()
    {
        if (CurrentTarget == null) { return; }

        cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    private void ClearTargetGroupList()
    {
        Transform playerTransform = cineTargetGroup.m_Targets[0].target;
        cineTargetGroup.m_Targets = new CinemachineTargetGroup.Target[0];
        cineTargetGroup.AddMember(playerTransform, 0.25f, 2f);
    }

    private void RemoveTarget(Target target)
    {
        if(CurrentTarget == target)
        {
            cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }
        
        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}

