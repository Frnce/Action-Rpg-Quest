using Advent.Entities;
using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Manager
{
    public class StatsManager : MonoBehaviour
    {
        public static StatsManager instance;
        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }
        public int InitMaxHP(EntitiesStats stats, float currentLevel = 1)
        {
            int value = Mathf.FloorToInt(Mathf.Round(((stats.base_Vit * 300f) + (stats.GetStat(BaseStat.BaseStatType.BONUS_VIT).GetCalculatedStatValue() * 125f) + (currentLevel * 0.5f)) / 2));
            return value;
        }
        public int InitMaxMP(EntitiesStats stats, float currentLevel = 1)
        {
            int value = Mathf.FloorToInt(Mathf.Round(((stats.base_Int * 150) + (stats.GetStat(BaseStat.BaseStatType.BONUS_INT).GetCalculatedStatValue() * 50) + (currentLevel * 0.5f)) / 2));
            return value;
        }
        public IntRange InitDamage(EntitiesStats stats,float currentLevel = 1)
        {
            IntRange value = new IntRange(0, 0);
            float damageUp = Mathf.Floor(Mathf.Round((stats.base_Str * 6f) + (stats.GetStat(BaseStat.BaseStatType.BONUS_STR).GetCalculatedStatValue() * 4f) + (currentLevel * 0.5f) / 2));
            value.m_Min = Mathf.FloorToInt(Mathf.Round(damageUp + stats.GetStat(BaseStat.BaseStatType.P_ATK_MIN).GetCalculatedStatValue()));
            value.m_Max = Mathf.FloorToInt(Mathf.RoundToInt(damageUp + stats.GetStat(BaseStat.BaseStatType.P_ATK_MAX).GetCalculatedStatValue()));
            return value;
        }
        public int InitPDef(EntitiesStats stats)
        {
            int value = Mathf.FloorToInt(Mathf.Round((stats.base_Str * 0.8f) + stats.GetStat(BaseStat.BaseStatType.P_DEF).GetCalculatedStatValue()));
            return value;
        }
        public int InitMDef(EntitiesStats stats)
        {
            int value = Mathf.FloorToInt(Mathf.Round((stats.base_Str * 0.5f) + stats.GetStat(BaseStat.BaseStatType.M_DEF).GetCalculatedStatValue()));
            return value;
        }

        public int GetCalculatedDamage(IntRange baseAttack,EntitiesStats stats,int targetDef)
        {
            int minDamage = 0;
            int maxDamage = 0;
            int finalDamage = 0;

            minDamage = baseAttack.m_Min;
            maxDamage = baseAttack.m_Max;

            minDamage += CalculateCrit(minDamage, stats.GetStat(BaseStat.BaseStatType.CRIT_CHANCE).GetCalculatedStatValue(), stats.GetStat(BaseStat.BaseStatType.CRIT_DMG_PERCENT).GetCalculatedStatValue());
            maxDamage += CalculateCrit(minDamage, stats.GetStat(BaseStat.BaseStatType.CRIT_CHANCE).GetCalculatedStatValue(), stats.GetStat(BaseStat.BaseStatType.CRIT_DMG_PERCENT).GetCalculatedStatValue());

            IntRange damageResult = new IntRange(Mathf.FloorToInt(Mathf.Round(minDamage)), Mathf.FloorToInt(Mathf.Round(maxDamage)));
            int baseDamage = damageResult.Random; //Get the very base of the damage from damage result;
            finalDamage = Mathf.FloorToInt(Mathf.Round((baseDamage + CalculatePDmgIncrease(baseDamage, stats.GetStat(BaseStat.BaseStatType.CRIT_DMG_PERCENT).GetCalculatedStatValue())) - targetDef));
            //Debug.Log(finalDamage);
            return finalDamage;
        }

        private int CalculateCrit(int damage, int critRate, int critDmgMultiplier)
        {
            if (Random.value <= (critRate / 100f))
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
    }
}