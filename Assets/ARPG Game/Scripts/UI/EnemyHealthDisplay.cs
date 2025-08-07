using System.Collections;
using System.Collections.Generic;
using ARPG.Attibutes;
using TMPro;
using UnityEngine;

public class EnemyHealthDisplay : MonoBehaviour
{
    Health health;

    public void SetTarget(Health healthToUse)
    {
        health = healthToUse;
    }

    // Update is called once per frame
    void Update()
    {
        if (health == null)
        {
            GetComponent<TextMeshProUGUI>().text = "No Target";
            return;
        }
        
        GetComponent<TextMeshProUGUI>().text = $"{health.GetHealthPercentage():0}%";
    }
}
