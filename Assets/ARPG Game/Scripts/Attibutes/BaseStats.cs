using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Attibutes
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 101)]
        [SerializeField] int startingLevel = -1;
        [SerializeField] CharacterClass characterClass = CharacterClass.Warrior;
        [SerializeField] Progression progression = null;

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
        }
    }
}
