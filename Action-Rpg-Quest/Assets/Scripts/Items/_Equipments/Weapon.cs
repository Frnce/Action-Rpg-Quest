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
        public EquipmentStats baseDamage;

        public Weapon()
        {
            slots = EquipSlots.WEAPON;
        }
            
        //public override IntRange GetStats()
        //{
        //    return baseDamage;
        //}
    }
}
