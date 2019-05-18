using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Manager
{
    public class PlayerLevelManager : MonoBehaviour
    {
        public static PlayerLevelManager instance;
        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        [SerializeField]
        private int maxLevel = 100;
        [SerializeField]
        private int currentLevel = 1;

        [SerializeField]
        private int experienceNeeded = 250;
        [SerializeField]
        private int currentExperience;

        public void GainExp(int expToAdd)
        {
            Debug.Log("GAINED EXP");
            currentExperience += expToAdd;
            while (currentExperience >= experienceNeeded)
            {
                experienceNeeded = Mathf.RoundToInt(100 * currentLevel * Mathf.Pow(currentLevel, 0.5f));
                if (currentLevel <= maxLevel)
                {
                    currentLevel++;
                    currentExperience = 0;
                    Debug.Log("Leveled UP! insert ff levelup music");
                }
            }
        }
        public int GetCurrentLevel
        {
            get
            {
                return currentLevel;
            }
        }
        public int GetExpNeeded
        {
            get
            {
                return experienceNeeded;
            }
        }
        public int GetCurrentExp
        {
            get
            {
                return currentExperience;
            }
        }
    }
}