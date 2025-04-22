using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public Target CurrentTarget { get; private set; }
    private List<Target> targets = new List<Target>();

    void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();
        if (target == null) { return; }
        if (target != null && !targets.Contains(target))
        {
            targets.Add(target);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Target target = other.GetComponent<Target>();
        if (target == null) { return; }
        if (target != null && targets.Contains(target))
        {
            targets.Remove(target);
        }
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }
        
        CurrentTarget = targets[0];
        return true;
    }

    public void Cancel()
    {
        CurrentTarget = null;
    }
}
