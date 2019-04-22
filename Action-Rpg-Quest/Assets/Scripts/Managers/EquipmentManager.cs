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
                //player.defense.AddModifier(newItem.defenseModifier);
                //player.attack.AddModifier(newItem.pAttackModifier);

                //if(newItem.weaponType == WeaponType.RANGE && newItem.equipSlot == EquipSlot.WEAPON)
                //{
                //    player.canRangeSingleAttack = true;
                //    player.rangeOfWeapon = newItem.weaponRange;
                //}
                //else
                //{
                //    player.canRangeSingleAttack = false;
                //}
            }
            if (oldItem != null)
            {
                //player.defense.RemoveModifier(oldItem.defenseModifier);
                //player.attack.RemoveModifier(oldItem.pAttackModifier);
            }
        }
        public void Equip(Equipment newItem)
        {
            int slotIndex = (int)newItem.GetSlots();
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