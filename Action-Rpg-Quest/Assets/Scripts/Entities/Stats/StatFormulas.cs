using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public class StatFormulas
    {
        public float ComputeMaxHP(float baseVit,float bonusVit, float level)
        {
            float result = ((baseVit * 300f) + (bonusVit * 125f) + (level * 0.5f)) / 2;
            return Mathf.RoundToInt(result);
        }
        public float ComputeMaxMP(float baseInt, float bonusInt,float level)
        {
            float result = ((baseInt * 150) + (bonusInt * 50) + (level * 0.5f)) / 2;
            return Mathf.RoundToInt(result);
        }
        public IntRange ComputeBaseAttack(float str,float lvl,float weaponMin,float weaponMax)
        {
            IntRange weaponDamage = new IntRange(0,0);
            float damageUp = (str * 1.6f); 

            weaponDamage.m_Min = Mathf.RoundToInt((damageUp + weaponMin) + (lvl * 6.0f));
            weaponDamage.m_Max = Mathf.RoundToInt((damageUp + weaponMax) + (lvl * 6.0f));

            return weaponDamage;
        }
    }
}