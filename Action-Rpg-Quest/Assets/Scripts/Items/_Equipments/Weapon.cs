using Advent.Entities;
using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(menuName = "Items/Equipment/Weapon", fileName = "New Weapon")]
    public class Weapon : Equipment
    {
        [Header("Weapon Data")]
        public AttackType attackType;
        public IntRange baseDamage = new IntRange(0, 0);

        private StatModifier minBaseStat;
        private StatModifier maxBaseStat;
        public Weapon()
        {
            slots = EquipSlots.WEAPON;
        }
        public override AttackType GetAttackType
        {
            get
            {
                return attackType;
            }
        }
        public override void AddStatsFromItem(Stats stats)
        {
            base.AddStatsFromItem(stats);
            minBaseStat = new StatModifier(baseDamage.m_Min, StatModType.FLAT);
            maxBaseStat = new StatModifier(baseDamage.m_Max, StatModType.FLAT);

            stats.weaponDamage.minDamage.AddModifier(minBaseStat);
            stats.weaponDamage.maxDamage.AddModifier(maxBaseStat);
        }
        public override void RemoveStatsFromItem(Stats stats)
        {
            base.RemoveStatsFromItem(stats);
            stats.weaponDamage.minDamage.RemoveModifier(minBaseStat);
            stats.weaponDamage.maxDamage.RemoveModifier(maxBaseStat);
        }
    }
}