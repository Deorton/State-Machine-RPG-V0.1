using System.Collections;
using System.Collections.Generic;
using ARPG.Attibutes;
using TMPro;
using UnityEngine;

namespace ARPG.UI
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            GetComponent<TextMeshProUGUI>().text = $"{health.GetHealthPercentage():0}%";
        }
    }
}
