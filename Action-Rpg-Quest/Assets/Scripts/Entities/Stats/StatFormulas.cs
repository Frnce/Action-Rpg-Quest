using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public class StatFormulas
    {
        #region HP Calculations
        public int ComputeMaxHP(float baseVit,float bonusVit, float level)
        {
            float result = Mathf.Floor(Mathf.Round(((baseVit * 300f) + (bonusVit * 125f) + (level * 0.5f)) / 2));
            return Mathf.RoundToInt(result);
        }
        #endregion
        #region MP Calculations
        public int ComputeMaxMP(float baseInt, float bonusInt,float level)
        {
            float result = Mathf.Floor(Mathf.Round(((baseInt * 150) + (bonusInt * 50) + (level * 0.5f)) / 2));
            return Mathf.RoundToInt(result);
        }
        #endregion
        #region Base Attack Calculation
        public IntRange ComputeBaseAttack(EntitiesStats entitiesStats, float lvl)
        {
            IntRange result = new IntRange(0, 0);
            float damageUp = Mathf.Floor(Mathf.Round((entitiesStats.base_Str * 6f) + (entitiesStats.GetStat(BaseStat.BaseStatType.BONUS_STR).GetCalculatedStatValue() * 4f) + (lvl * 0.5f) / 2));
            result.m_Min = Mathf.FloorToInt(Mathf.Round(damageUp + entitiesStats.GetStat(BaseStat.BaseStatType.P_ATK_MIN).GetCalculatedStatValue()));
            result.m_Max = Mathf.FloorToInt(Mathf.RoundToInt(damageUp + entitiesStats.GetStat(BaseStat.BaseStatType.P_ATK_MAX).GetCalculatedStatValue()));

            return result;
        }
        #endregion
        #region Max Defense Calculation
        public int ComputeMaxDefense(EntitiesStats entitiesStats)
        {
            int result = Mathf.FloorToInt(Mathf.Round((entitiesStats.base_Str * 0.8f) + entitiesStats.GetStat(BaseStat.BaseStatType.P_Def).GetCalculatedStatValue()));

            return result;
        }
        #endregion
        #region Damage Calculations
        public int ComputeDamage(IntRange baseAttack,EntitiesStats stats,int targetDef)
        {
            //TODO FIX DAMAGE
            int minDamage = 0;
            int maxDamage = 0;
            int finalDamage = 0;

            minDamage = baseAttack.m_Min;
            maxDamage = baseAttack.m_Max;

            minDamage += CalculateCrit(minDamage,stats.GetStat(BaseStat.BaseStatType.CRIT_CHANCE).GetCalculatedStatValue(),stats.GetStat(BaseStat.BaseStatType.CRIT_DMG_PERCENT).GetCalculatedStatValue());
            maxDamage += CalculateCrit(minDamage, stats.GetStat(BaseStat.BaseStatType.CRIT_CHANCE).GetCalculatedStatValue(), stats.GetStat(BaseStat.BaseStatType.CRIT_DMG_PERCENT).GetCalculatedStatValue());

            IntRange damageResult = new IntRange(Mathf.FloorToInt(Mathf.Round(minDamage)), Mathf.FloorToInt(Mathf.Round(maxDamage)));
            int baseDamage = damageResult.Random; //Get the very base of the damage from damage result;
            finalDamage = Mathf.FloorToInt(Mathf.Round((baseDamage + CalculatePDmgIncrease(baseDamage, stats.GetStat(BaseStat.BaseStatType.CRIT_DMG_PERCENT).GetCalculatedStatValue())) - targetDef));
            Debug.Log(finalDamage);
            return finalDamage;
        }

        private int CalculateCrit(int damage,int critRate , int critDmgMultiplier)
        {
            if(Random.value <= (critRate / 100f))
            {
                int critDamage = Mathf.FloorToInt(Mathf.Round(damage * (critDmgMultiplier / 100f)));
                return critDamage;
            }
            return 0;
        }
        private float CalculatePDmgIncrease(int damage, int Dmg_Multiplier)
        {
            float multiplier = damage * (Dmg_Multiplier / 100f);
            if (multiplier == 0)
            {
                return 0;
            }
            float result = damage + multiplier;
            return result;
        }
        #endregion
        // Base Dmg (str)
        // Weapon Dmg
        // Mods 
        // P.dmg Increase %
        // 
        // Defense
        // Special Defense
        // Crit Chance  & Damage()
    }
}