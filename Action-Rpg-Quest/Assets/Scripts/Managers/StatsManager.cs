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
        public void InitStats(Stats stats)
        {
            //Basic Attribute
            stats.bonusSTR = new EntityStat();
            stats.bonusDEX = new EntityStat();
            stats.bonusINT = new EntityStat();
            stats.bonusVIT = new EntityStat();

            //HP MP
            stats.maxHitPoints = new EntityStat();
            stats.maxManaPoints = new EntityStat();

            stats.weaponDamage.minDamage = new EntityStat();
            stats.weaponDamage.maxDamage = new EntityStat();
            stats.baseAttack = new IntRange(0, 0);

            stats.armorDefense = new EntityStat();
            //stats.magicalDefense = new EntityStat();

            //Movement Speed
            stats.movementSpeed = new EntityStat();

            //stats.criticalHitChance = new EntityStat();
            //stats.criticalHitDamage = new EntityStat();

            //stats.ignorePhysicalDefense = new EntityStat();
            //stats.ignoreMagicalDefense = new EntityStat();

            //stats.healthRegen = new EntityStat();
            //stats.manaRegen = new EntityStat();

            //stats.lifeStealPercent = new EntityStat();
            //stats.abilityCooldownReduction = new EntityStat();
        }
        public void InitMaxHP(Stats statList,StatFormulas statFormula,float currentLevel = 1)
        {
            statList.maxHitPoints.baseValue = statFormula.ComputeMaxHP(statList.baseVIT,statList.bonusVIT.getValue,currentLevel);
        }
        public void InitMaxMP(Stats statList, StatFormulas statFormula, float currentLevel = 1)
        {
            statList.maxManaPoints.baseValue = statFormula.ComputeMaxMP(statList.baseINT, statList.bonusINT.getValue, currentLevel);
        }
    }
}