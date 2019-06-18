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
            DontDestroyOnLoad(gameObject);
        }
        //public void InitStats(Stats stats)
        //{
        //    //Basic Attribute
        //    stats.bonusSTR = new EntityStat();
        //    stats.bonusDEX = new EntityStat();
        //    stats.bonusINT = new EntityStat();
        //    stats.bonusVIT = new EntityStat();

        //    //HP MP
        //    stats.maxHitPoints = new EntityStat();
        //    stats.maxManaPoints = new EntityStat();

        //    stats.weaponDamage.minDamage = new EntityStat();
        //    stats.weaponDamage.maxDamage = new EntityStat();
        //    stats.baseAttack = new IntRange(0, 0);

        //    stats.armorDefense = new EntityStat();
        //    //stats.magicalDefense = new EntityStat();

        //    //Movement Speed
        //    stats.movementSpeed = new EntityStat();

        //    stats.PdmgIncreaseMod = new EntityStat();

        //    //stats.criticalHitChance = new EntityStat();
        //    //stats.criticalHitDamage = new EntityStat();

        //    //stats.ignorePhysicalDefense = new EntityStat();
        //    //stats.ignoreMagicalDefense = new EntityStat();

        //    //stats.healthRegen = new EntityStat();
        //    //stats.manaRegen = new EntityStat();

        //    //stats.lifeStealPercent = new EntityStat();
        //    //stats.abilityCooldownReduction = new EntityStat();
        //}
        public int InitMaxHP(EntitiesStats stats, StatFormulas statFormula, float currentLevel = 1)
        {
            int value = statFormula.ComputeMaxHP(stats.base_Vit, stats.GetStat(BaseStat.BaseStatType.BONUS_VIT).GetCalculatedStatValue(), currentLevel);
            return value;
        }
        public int InitMaxMP(EntitiesStats stats, StatFormulas statFormula, float currentLevel = 1)
        {
            int value = statFormula.ComputeMaxMP(stats.base_Int, stats.GetStat(BaseStat.BaseStatType.BONUS_INT).GetCalculatedStatValue(), currentLevel);
            return value;
        }
    }
}