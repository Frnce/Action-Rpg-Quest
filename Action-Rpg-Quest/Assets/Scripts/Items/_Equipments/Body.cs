using Advent.Entities;
using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(menuName = "Items/Equipment/Body", fileName = "New Body")]
    public class Body : Equipment
    {
        [Header("Body Data")]
        public int baseArmor;

        private StatModifier baseStatMod;
        public Body()
        {
            slots = EquipSlots.BODY;
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