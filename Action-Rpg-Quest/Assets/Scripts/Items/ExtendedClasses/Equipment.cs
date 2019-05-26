using Advent.Manager;
using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    public class Equipment : Item
    {
        [Header("Equipment Data")]
        public EquipSlots slots;
        public AttackType attackType;

        public Equipment()
        {
            itemType = ItemTypes.EQUIPMENTS;
            stackSize = -1;
        }

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

        public virtual EquipmentStats GetStats()
        {
            return new EquipmentStats();
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
        ACCESORY, //6
    }
    public enum AttackType
    {
        SLASH,
        SHOOT,
        NONE
    }
    [System.Serializable]
    public class EquipmentStats
    {
        public StatType statType;
        [Space]
        public int fixedValue;
        public IntRange rangedValue = new IntRange(0, 0);

        public EquipmentStats(){}
        public EquipmentStats(StatType _statType,int _value)
        {
            statType = _statType;
            fixedValue = _value;
        }
        public EquipmentStats(StatType _statType,IntRange _value)
        {
            statType = _statType;
            rangedValue = _value;
        }
    }
    public enum StatType
    {
        ATTACK,
        DEFENSE
    }
}