using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Attibutes
{
    [CreateAssetMenu(fileName = "Progression", menuName = "ARPG/Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if(progressionClass.characterClass == characterClass)
                {
                    return progressionClass.health[level - 1];
                }
            }
            return 0f; // Default value if not found
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public float[] health;
        }
    }
}
