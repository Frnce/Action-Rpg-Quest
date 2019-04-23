using Advent.Manager;
using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(menuName ="Items/Equipment",fileName = "New Equipment")]
    public class Equipment : Item
    {
        public EquipSlots slots;
        public StatRange attack;
        public StatRange defense;
        public override void Use()
        {
            base.Use();
            EquipmentManager.instance.Equip(this);
        }
        public void Unequip(int index)
        {
            EquipmentManager.instance.SwapEquip(index);
        }
        public EquipSlots GetSlots
        {
            get
            {
                return slots;
            }
        }
    }
    public enum EquipSlots
    {
        ARM,
        WEAPON
    }
}