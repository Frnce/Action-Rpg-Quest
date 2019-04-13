using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Utilities
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField]
        private int baseValue = 0;
        private List<int> modifiers = new List<int>();
        public void AddStat(int stat)
        {
            baseValue += stat;
        }
        public void MinusStat(int stat)
        {
            baseValue -= stat;
        }
        public int GetValue()
        {
            int finalValue = baseValue;
            modifiers.ForEach(x => finalValue += x);
            return finalValue;
        }
        public void AddModifier(int modifier)
        {
            if (modifier != 0)
            {
                modifiers.Add(modifier);
            }
        }
        public void RemoveModifier(int modifier)
        {
            if (modifier != 0)
            {
                modifiers.Remove(modifier);
            }
        }
    }
}