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
        public AttackType attackType;
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
        WEAPON,
        HEAD,
        BODY,
        LEGS,
        FOOT,
        ARMS,
        NECKLACE,
        RING1,
        RING2
    }
    public enum AttackType
    {
        SLASH,
        SHOOT,
        NONE
    }
}