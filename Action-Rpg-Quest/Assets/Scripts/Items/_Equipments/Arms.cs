using Advent.Entities;
using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(menuName = "Items/Equipment/Arms", fileName = "New Arms")]
    public class Arms : Equipment
    {
        [Header("Arms Data")]
        public int baseArmor;

        private StatModifier baseStatMod;

        public Arms()
        {
            slots = EquipSlots.ARMS;
        }
        public override void AddStatsFromItem(Stats stats)
        {
            base.AddStatsFromItem(stats);
            baseStatMod = new StatModifier(baseArmor, StatModType.FLAT);

            stats.armorDefense.AddModifier(baseStatMod);
        }
        public override void RemoveStatsFromItem(Stats stats)
        {
            base.RemoveStatsFromItem(stats);
            stats.armorDefense.RemoveModifier(baseStatMod);
        }
    }
}