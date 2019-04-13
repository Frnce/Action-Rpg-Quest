using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Utilities
{
    [System.Serializable]
    public class IntRange
    {
        public int m_Min;
        public int m_Max;

        public IntRange(int min, int max)
        {
            m_Min = min;
            m_Max = max;
        }

        public int Random
        {
            get { return UnityEngine.Random.Range(m_Min, m_Max); }
        }
    }
}
