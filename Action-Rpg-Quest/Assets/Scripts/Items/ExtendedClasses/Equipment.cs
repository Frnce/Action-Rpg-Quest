using Advent.Entities;
using Advent.Enums;
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

        public Equipment()
        { 
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
        public virtual AttackType GetAttackType
        {
            get
            {
                return AttackType.NONE;
            }
        }
        public virtual void AddStatsFromItem(Stats stats)
        {
            Debug.Log("Add Stats to "+ stats);
        }
        public virtual void RemoveStatsFromItem(Stats stats)
        {
            Debug.Log("Removing  Stats to "+ stats);
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
    public class StatModRange
    {
        public StatModifier statMin;
        public StatModifier statMax;
        
        public StatModRange(StatModifier _min, StatModifier _max)
        {
            statMin = _min;
            statMax = _max;
        }
    }
}