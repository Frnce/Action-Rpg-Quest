using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Utilities
{
    [System.Serializable]
    public class StatRange
    {
        [SerializeField]
        private IntRange baseValue = new IntRange(0, 0);
        private List<IntRange> modifiers = new List<IntRange>();
        public void AddStat(IntRange stat)
        {
            baseValue.m_Min += stat.m_Min;
            baseValue.m_Max += stat.m_Max;
        }
        public void MinusStat(IntRange stat)
        {
            baseValue.m_Min -= stat.m_Min;
            baseValue.m_Max -= stat.m_Max;
        }
        public IntRange GetRealValue()
        {
            IntRange finalValue = baseValue;
            modifiers.ForEach(x => finalValue.m_Min += x.m_Min);
            modifiers.ForEach(x => finalValue.m_Max += x.m_Max);
            return finalValue;
        }
        public int GetMinValue()
        {
            IntRange finalValue = baseValue;
            modifiers.ForEach(x => finalValue.m_Min += x.m_Min);
            return finalValue.m_Min;
        }
        public int GetMaxValue()
        {
            IntRange finalValue = baseValue;
            modifiers.ForEach(x => finalValue.m_Max += x.m_Max);
            return finalValue.m_Max;
        }
        public int GetRandomValue()
        {
            IntRange finalValue = baseValue;
            modifiers.ForEach(x => finalValue.m_Min += x.m_Min);
            modifiers.ForEach(x => finalValue.m_Max += x.m_Max);
            return finalValue.Random;
        }
        public void AddModifier(IntRange modifier)
        {
            if (modifier != null)
            {
                modifiers.Add(modifier);
            }
        }
        public void RemoveModifier(IntRange modifier)
        {
            if (modifier != null)
            {
                modifiers.Remove(modifier);
            }
        }
    }
}