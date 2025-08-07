using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Attibutes
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float experiencePoints = 0;

        public void GainExperience(float experienceToAdd)
        {
            experiencePoints += experienceToAdd;
            Debug.Log($"{gameObject.name} gained {experienceToAdd} experience points. Total: {experiencePoints}");
        }
    }
}
