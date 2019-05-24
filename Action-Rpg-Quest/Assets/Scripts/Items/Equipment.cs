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

        public IntRange damage = new IntRange(5, 13);

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
        HEAD, //0
        BODY, //1
        WEAPON, //2
        ARMS, //3 
        LEGS, //4
        FOOT, //5
        ACCESORRY1, //6
        ACCESORRY2, //7
    }
    public enum AttackType
    {
        SLASH,
        SHOOT,
        NONE
    }
}