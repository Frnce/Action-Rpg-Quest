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
                AddAttackAction(item);
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
        public void SwapItem(Item newItem,Item oldItem)
        {
            UnequipItem(oldItem);
            EquipItem(newItem);
        }
        public void AddAttackAction(Item item)
        {
            if (item.WeaponType != WeaponTypes.NONE)
            {
                switch (item.WeaponType)
                {
                    case WeaponTypes.DAGGER:
                        Debug.Log("Equipped Dagger");
                        break;
                    case WeaponTypes.SHORTSWORD:
                        Debug.Log("Equipped Short Sword");
                        break;
                    default:
                        break;
                }
            }
        }

        public EquipmentSlot[] GetEquipsList
        {
            get
            {
                return equipsList;
            }
        }
    }
}