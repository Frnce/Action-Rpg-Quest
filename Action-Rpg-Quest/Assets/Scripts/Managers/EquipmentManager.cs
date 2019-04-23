using Advent.Entities;
using Advent.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Manager
{
    public class EquipmentManager : MonoBehaviour
    {
        public static EquipmentManager instance;
        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }

        public Equipment[] currentEquipment;
        public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
        public OnEquipmentChanged onEquipmentChangedCallback;
        Player player;
        InventoryManager inventory;

        void Start()
        {
            player = Player.instance;
            inventory = InventoryManager.instance;
            int numOfSlots = System.Enum.GetNames(typeof(EquipSlots)).Length;
            currentEquipment = new Equipment[numOfSlots];

            onEquipmentChangedCallback += OnEquipmentChange;
        }
        public void OnEquipmentChange(Equipment newItem, Equipment oldItem)
        {
            if (newItem != null)
            {
                player.GetStats.defense.AddModifier(newItem.defense.GetRealValue());
                player.GetStats.attack.AddModifier(newItem.attack.GetRealValue());
            }
            if (oldItem != null)
            {
                player.GetStats.defense.RemoveModifier(oldItem.defense.GetRealValue());
                player.GetStats.attack.RemoveModifier(oldItem.attack.GetRealValue());
            }
        }
        public void Equip(Equipment newItem)
        {
            int slotIndex = (int)newItem.GetSlots;
            Equipment oldItem = null;
            if (currentEquipment[slotIndex] != null)
            {
                oldItem = currentEquipment[slotIndex];
                inventory.AddItem(oldItem);
            }

            if (onEquipmentChangedCallback != null)
            {
                onEquipmentChangedCallback.Invoke(newItem, oldItem);
            }

            currentEquipment[slotIndex] = newItem;
        }

        public void SwapEquip(int slotIndex)
        {
            if (currentEquipment[slotIndex] != null)
            {
                Equipment oldItem = currentEquipment[slotIndex];
                inventory.AddItem(oldItem);
                currentEquipment[slotIndex] = null;

                if (onEquipmentChangedCallback != null)
                {
                    onEquipmentChangedCallback.Invoke(null, oldItem);
                }
            }
        }
    }
}