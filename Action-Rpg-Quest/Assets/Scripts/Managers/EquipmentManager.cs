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
    [System.Serializable]
    public struct EquipmentSlot
    {
        public Item item;
        public ItemTypes itemtype;
    }
    public class EquipmentManager : MonoBehaviour
    {
        public static EquipmentManager instance;

        [SerializeField]
        private EquipmentSlot[] equipsList;
        

        //public SpriteRenderer weaponSpriteRenderer;
        ////Add more spriteRenderer for player Here;
        //public Equipment[] defaultEquipments;
        //public Equipment[] currentEquipment;
        //public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
        //public OnEquipmentChanged onEquipmentChangedCallback;
        //Player player;
        //InventoryManager inventory;

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

            equipsList = new EquipmentSlot[6]; //6 number of equipmentSlots
        }

        public bool EquipItem(Item item)
        {
            int equipSlotIndex = (int)item.EquipType;
            if(equipSlotIndex <= equipsList.Length)
            {
                equipsList[equipSlotIndex].item = item;
                InventoryManager.instance.RemoveItem(item);
                UIEventHandlers.EquipUpdate();
                //Stat Changes Here
                return true;
            }
            return false;
        }
        public bool UnequipItem(Item item)
        {
            int equipSlotIndex = (int)item.EquipType;
            if (equipSlotIndex <= equipsList.Length)
            {
                equipsList[equipSlotIndex].item = null;
                InventoryManager.instance.RemoveItem(item);
                UIEventHandlers.EquipUpdate();
                //Stat Changes Here
                return true;
            }
            return false;
        }

        public EquipmentSlot[] GetEquipsList
        {
            get
            {
                return equipsList;
            }
        }

        //void Start()
        //{
        //    player = Player.instance;
        //    inventory = InventoryManager.instance;
        //    int numOfSlots = System.Enum.GetNames(typeof(EquipSlots)).Length;
        //    currentEquipment = new Equipment[numOfSlots];
        //}
        //private void AddStatsOnEquip(Equipment item)
        //{
        //    item.AddStatsFromItem(player.GetStats);
        //}
        //private void RemoveStatsOnEquip(Equipment item)
        //{
        //    item.RemoveStatsFromItem(player.GetStats);
        //}
        //public void OnEquipmentChange(Equipment newItem, Equipment oldItem)
        //{
        //    //Add New Stats on EquipmentChange
        //    if (newItem != null)
        //    {
        //        AddStatsOnEquip(newItem);
        //    }
        //    if (oldItem != null)
        //    {
        //        RemoveStatsOnEquip(oldItem);
        //    }
        //}
        //public void Equip(Equipment newItem)
        //{
        //    int slotIndex = (int)newItem.GetSlots;
        //    Equipment oldItem = null;
        //    if (currentEquipment[slotIndex] != null)
        //    {
        //        oldItem = currentEquipment[slotIndex];
        //        //inventory.AddItem(oldItem);
        //    }

        //    if (onEquipmentChangedCallback != null)
        //    {
        //        onEquipmentChangedCallback.Invoke(newItem, oldItem);
        //    }

        //    currentEquipment[slotIndex] = newItem;

        //    switch (newItem.slots)
        //    {
        //        case EquipSlots.WEAPON:
        //            //weaponSpriteRenderer.sprite = newItem.icon;
        //            break;
        //        case EquipSlots.HEAD:
        //            break;
        //        case EquipSlots.BODY:
        //            break;
        //        case EquipSlots.LEGS:
        //            break;
        //        case EquipSlots.FOOT:
        //            break;
        //        case EquipSlots.ARMS:
        //            break;
        //        case EquipSlots.ACCESORY:
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //public void SwapEquip(int slotIndex)
        //{
        //    if (currentEquipment[slotIndex] != null)
        //    {
        //        Equipment oldItem = currentEquipment[slotIndex];
        //        //inventory.AddItem(oldItem);
        //        currentEquipment[slotIndex] = null;

        //        if (onEquipmentChangedCallback != null)
        //        {
        //            onEquipmentChangedCallback.Invoke(null, oldItem);
        //        }
        //    }
        //}

        //public void EquipDefaults()
        //{
        //    if (defaultEquipments.Length >= 0)
        //    {
        //        for (int i = 0; i < defaultEquipments.Length; i++)
        //        {
        //            Equip(defaultEquipments[i]);
        //        }
        //    }
        //}
    }
}