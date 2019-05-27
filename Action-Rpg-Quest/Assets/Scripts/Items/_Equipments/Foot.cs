using Advent.Entities;
using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(menuName = "Items/Equipment/Foot", fileName = "Foot")]
    public class Foot : Equipment
    {
        [Header("Foot Data")]
        public int baseArmor;

        private StatModifier baseStatMod;
        public Foot()
        {
            slots = EquipSlots.FOOT;
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