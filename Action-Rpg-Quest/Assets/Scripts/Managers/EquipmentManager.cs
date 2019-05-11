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
        public SpriteRenderer weaponSpriteRenderer;
        //Add more spriteRenderer for player Here;

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

            switch (newItem.slots)
            {
                case EquipSlots.WEAPON:
                    weaponSpriteRenderer.sprite = newItem.icon;
                    break;
                case EquipSlots.HEAD:
                    break;
                case EquipSlots.BODY:
                    break;
                case EquipSlots.LEGS:
                    break;
                case EquipSlots.FOOT:
                    break;
                case EquipSlots.ARMS:
                    break;
                case EquipSlots.NECKLACE:
                    break;
                case EquipSlots.RING1:
                    break;
                case EquipSlots.RING2:
                    break;
                default:
                    break;
            }
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