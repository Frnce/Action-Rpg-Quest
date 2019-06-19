using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public class StatFormulas
    {
        public int ComputeMaxHP(float baseVit,float bonusVit, float level)
        {
            float result = Mathf.Floor(Mathf.Round(((baseVit * 300f) + (bonusVit * 125f) + (level * 0.5f)) / 2));
            return Mathf.RoundToInt(result);
        }
        public int ComputeMaxMP(float baseInt, float bonusInt,float level)
        {
            float result = Mathf.Floor(Mathf.Round(((baseInt * 150) + (bonusInt * 50) + (level * 0.5f)) / 2));
            return Mathf.RoundToInt(result);
        }
        public IntRange ComputeBaseAttack(EntitiesStats entitiesStats, float lvl)
        {
            IntRange result = new IntRange(0, 0);
            float damageUp = Mathf.Floor(Mathf.Round((entitiesStats.base_Str * 6f) + (entitiesStats.GetStat(BaseStat.BaseStatType.BONUS_STR).GetCalculatedStatValue() * 4f) + (lvl * 0.5f) / 2));
            result.m_Min = Mathf.FloorToInt(Mathf.Round(damageUp + entitiesStats.GetStat(BaseStat.BaseStatType.P_ATK_MIN).GetCalculatedStatValue()));
            result.m_Max = Mathf.FloorToInt(Mathf.RoundToInt(damageUp + entitiesStats.GetStat(BaseStat.BaseStatType.P_ATK_MAX).GetCalculatedStatValue()));

            return result;
        }
        public int ComputeMaxDefense(EntitiesStats entitiesStats)
        {
            int result = Mathf.FloorToInt(Mathf.Round((entitiesStats.base_Str * 0.8f) + entitiesStats.GetStat(BaseStat.BaseStatType.Phy_Defense).GetCalculatedStatValue()));

            return result;
        }
        public int ComputeDamage(IntRange baseAttack,EntitiesStats stats,int targetDef)
        {
            //TODO FIX DAMAGE
            int minDamage = 0;
            int maxDamage = 0;

            minDamage = baseAttack.m_Min;
            maxDamage = baseAttack.m_Max;

            minDamage += CalculateCrit(minDamage,stats.GetStat(BaseStat.BaseStatType.CRIT_CHANCE).GetCalculatedStatValue(),stats.GetStat(BaseStat.BaseStatType.CRIT_DMG_PERCENT).GetCalculatedStatValue());
            maxDamage += CalculateCrit(minDamage, stats.GetStat(BaseStat.BaseStatType.CRIT_CHANCE).GetCalculatedStatValue(), stats.GetStat(BaseStat.BaseStatType.CRIT_DMG_PERCENT).GetCalculatedStatValue());

            IntRange damageResult = new IntRange(Mathf.FloorToInt(Mathf.Round(minDamage)), Mathf.FloorToInt(Mathf.Round(maxDamage)));
            int finalDamage = Mathf.FloorToInt(Mathf.Round((damageResult.Random * (stats.GetStat(BaseStat.BaseStatType.P_DMG_INCREASE).GetCalculatedStatValue() / 100f)) - targetDef));
            Debug.Log(finalDamage);
            return finalDamage;
        }

        private int CalculateCrit(int damage,int critRate , int critDmgMultiplier)
        {
            if(Random.value <= (critRate / 100f))
            {
                int critDamage = Mathf.FloorToInt(Mathf.Round(damage * critDmgMultiplier));
                return critDamage;
            }
            return 0;
        }
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