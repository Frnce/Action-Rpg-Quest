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
        public IntRange ComputeBaseAttack(float baseStr,float bonusStr,float lvl)
        {
            IntRange result = new IntRange(0,0);
            float damageUp = ((baseStr * 6f) + (bonusStr * 4f) + (lvl * 0.5f)) / 2;
            result.m_Min = Mathf.RoundToInt(damageUp);
            result.m_Max = Mathf.RoundToInt(damageUp);
            return result;
        }
        public int ComputeMaxDefense(float baseStr, float armorDefense)
        {
            int result = Mathf.RoundToInt((baseStr * 0.8f) + armorDefense);

            return result;
        }
        public int ComputeDamage(IntRange baseAttack, AttackDamageRange weaponAttack, int defense,int targetLevel)
        {
            float minDamage = baseAttack.m_Min + weaponAttack.minDamage.getValue;
            float maxDamage = baseAttack.m_Max + weaponAttack.maxDamage.getValue;

            Debug.Log("min Weapon : " + weaponAttack.minDamage.getValue + "Max Weapon : " + weaponAttack.maxDamage.getValue);

            IntRange damage = new IntRange(Mathf.RoundToInt(minDamage), Mathf.RoundToInt(maxDamage));
            float finalDamage = damage.Random - ((defense * 0.5f) + (targetLevel * 0.5f));
            return Mathf.RoundToInt(finalDamage);
        }
    }
}