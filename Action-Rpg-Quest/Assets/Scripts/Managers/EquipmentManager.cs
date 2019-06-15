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
        public PlayerWeaponController playerEquipment;
        [SerializeField]
        private Item[] equipmentSlots;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(gameObject);

            equipmentSlots = new Item[6]; //6 number of equipmentSlots
        }

        public bool EquipItem(Item item)
        {
            int equipSlotIndex = (int)item.EquipType;
            if(equipSlotIndex <= equipmentSlots.Length)
            {
                equipmentSlots[equipSlotIndex] = item;
                InventoryManager.instance.RemoveItem(item);
                playerEquipment.UnequipEquipment();
                playerEquipment.EquipWeapon(item);
                UIEventHandlers.EquipUpdate();
                return true;
            }
            return false;
        }
        public bool UnequipItem(Item item)
        {
            int equipSlotIndex = (int)item.EquipType;
            if (equipSlotIndex <= equipmentSlots.Length)
            {
                equipmentSlots[equipSlotIndex] = null;
                InventoryManager.instance.RemoveItem(item);
                playerEquipment.UnequipEquipment();
                playerEquipment.EnableBareHands();
                UIEventHandlers.EquipUpdate();
                //Stat Changes Here
                return true;
            }
            return false;
        }
        public void SwapItem(Item newItem, Item oldItem)
        {
            UnequipItem(oldItem);
            EquipItem(newItem);
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