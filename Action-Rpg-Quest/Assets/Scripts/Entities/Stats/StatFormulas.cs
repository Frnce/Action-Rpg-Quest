using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public class StatFormulas
    {
        public float ComputeMaxHP(float vitality, float level, float multiplier)
        {
            return (vitality + level) * multiplier; //TODO Add entity level to the equation HP
        }
        public float ComputeMaxMP(float intelligence, float level, float multiplier)
        {
            return (intelligence + level) * multiplier; //TODO Add entity level to the equation MP
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