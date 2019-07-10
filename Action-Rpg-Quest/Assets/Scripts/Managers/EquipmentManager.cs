using Advent.Entities;
using Advent.Entities.Abilities;
using Advent.Enums;
using Advent.Items;
using Advent.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Manager
{
    public class EquipmentManager : MonoBehaviour
    {
        public static EquipmentManager instance;
        [SerializeField]
        private Item[] equipmentSlots;

        public delegate void EquipChangeEventHandler();
        public static event EquipChangeEventHandler OnEquipChange;

        private void Awake()
        {
            instance = this;

            equipmentSlots = new Item[6]; //6 number of equipmentSlots
        }
        public bool EquipItem(Item item)
        {
            PlayerWeaponController playerEquipment = Player.instance.GetPlayerWeaponController;

            int equipSlotIndex = (int)item.EquipType;
            if(equipSlotIndex <= equipmentSlots.Length)
            {
                equipmentSlots[equipSlotIndex] = item;
                InventoryManager.instance.RemoveItem(item);
                if(item.EquipType == EquipTypes.WEAPON)
                {
                    playerEquipment.UnequipEquipment();
                    playerEquipment.EquipWeapon(item);
                }
                UIEventHandlers.EquipUpdate();
                Player.instance.GetPlayerStats().AddStatBonus(item.Stats);
                EquipChanged(); //invokes equipchange and stat changes;
                return true;
            }
            return false;
        }
        public bool UnequipItem(Item item)
        {
            PlayerWeaponController playerEquipment = Player.instance.GetPlayerWeaponController;

            int equipSlotIndex = (int)item.EquipType;
            if (equipSlotIndex <= equipmentSlots.Length)
            {
                equipmentSlots[equipSlotIndex] = null;
                InventoryManager.instance.RemoveItem(item);
                if(item.EquipType == EquipTypes.WEAPON)
                {
                    playerEquipment.UnequipEquipment();
                    playerEquipment.EnableBareHands();
                }
                Player.instance.GetPlayerStats().RemoveStatBonus(item.Stats);
                UIEventHandlers.EquipUpdate();
                EquipChanged();
                return true;
            }
            return false;
        }
        public void SwapItem(Item newItem, Item oldItem)
        {
            UnequipItem(oldItem);
            EquipItem(newItem);
        }

        public void EquipChanged() //Invocation for updating EquipmentStats
        {
            if(OnEquipChange != null)
            {
                OnEquipChange.Invoke();
            }
        }
        public Item[] GetEquipsList
        {
            get
            {
                return equipmentSlots;
            }
        }
    }
}