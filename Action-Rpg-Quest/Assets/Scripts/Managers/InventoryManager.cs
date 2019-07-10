using Advent.Enums;
using Advent.Items  ;
using Advent.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Advent.Manager
{
    [System.Serializable]
    public struct InventorySlot
    {
        public Item item;
        public int stackCount;
    }
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;
        [SerializeField]
        private int maxItemSpace = 50;
        [SerializeField]
        private int maxStack = 9;
        [SerializeField]
        private InventorySlot[] equipmentList;
        [SerializeField]
        private InventorySlot[] consumableList;
        [SerializeField]
        private InventorySlot[] materialsList;
        //[SerializeField]
        //private List<ItemsSpace> enchantList = new List<ItemsSpace>();
        //[SerializeField]
        //private List<ItemsSpace> materialList = new List<ItemsSpace>();
        [Space]
        [SerializeField]
        private int moneyAcquired = 0;

        private void Awake()
        {
            instance = this;

            equipmentList = new InventorySlot[maxItemSpace];
            consumableList = new InventorySlot[maxItemSpace];
            materialsList = new InventorySlot[maxItemSpace];
        }
        public void InitInventoryManager()
        {
            //Add SetItem to equip default item Here
            //GiveItem("Wpn_HuntingKnife");
            //GiveItem("Cons_Sm_RedPotion");
            //GiveItem("Wpn_GreatSword");
            //GiveItem("Bdy_IronTunic");
            //GiveItem("Bdy_Robe");
            //GiveItem("Hd_Cap");
            //GiveItem("Hd_Helmet");
            //GiveItem("Lg_Pants");
            //GiveItem("Lg_Leggings");
        }

        public bool AddToFirstEmptySlot(Item item)
        {
            if(item != null)
            {
                switch (item.ItemType)
                {
                    case ItemTypes.CONSUMABLE:
                        AddItemToConsumableTab(item);
                        break;
                    case ItemTypes.EQUIPMENTS:
                        AddItemToEquipmentTab(item);
                        break;
                    case ItemTypes.MATERIALS:
                        AddItemToMaterialsTab(item);
                        break;
                    default:
                        break;
                }
            }
            return false;
        }
        private bool AddItemToEquipmentTab(Item item)
        {
            for (int i = 0; i < equipmentList.Length; i++)
            {
                if (equipmentList[i].item == null)
                {
                    equipmentList[i].item = item;
                    UIEventHandlers.InventoryUpdate();
                    return true;
                }
            }
            return false;
        }
        private bool AddItemToConsumableTab(Item item)
        {
            for (int i = 0; i < consumableList.Length; i++)
            {
                if(consumableList[i].item == null)
                {
                    consumableList[i].item = item;
                    UIEventHandlers.InventoryUpdate();
                    return true;
                }
            }
            return false;
        }
        private bool AddItemToMaterialsTab(Item item)
        {
            for (int i = 0; i < materialsList.Length; i++)
            {
                if(materialsList[i].item == null)
                {
                    materialsList[i].item = item;
                    UIEventHandlers.InventoryUpdate();
                    return true;
                }
            }
            return false;
        }
        //Equipment
        public Item ReplaceItemInEquipmentSlot(Item item,int slot)
        {
            var oldItem = equipmentList[slot].item;
            equipmentList[slot].item = item;
            UIEventHandlers.InventoryUpdate();
            return oldItem;
        }
        public Item PopItemFromEquipmentSlot(int slot)
        {
            var item = equipmentList[slot].item;
            equipmentList[slot].item = null;
            UIEventHandlers.InventoryUpdate();
            return item;
        }
        //Consumables
        public Item ReplaceItemInConsumablesSlot(Item item, int slot)
        {
            var oldItem = consumableList[slot].item;
            consumableList[slot].item = item;
            UIEventHandlers.InventoryUpdate();
            return oldItem;
        }
        public Item PopItemFromnConsumablesSlot(int slot)
        {
            var item = consumableList[slot].item;
            consumableList[slot].item = null;
            UIEventHandlers.InventoryUpdate();
            return item;
        }
        //Materials
        public Item ReplaceItemInMaterialsSlot(Item item, int slot)
        {
            var oldItem = materialsList[slot].item;
            materialsList[slot].item = item;
            UIEventHandlers.InventoryUpdate();
            return oldItem;
        }
        public Item PopItemFromMaterialsSlot(int slot)
        {
            var item = materialsList[slot].item;
            materialsList[slot].item = null;
            UIEventHandlers.InventoryUpdate();
            return item;
        }
        public bool GiveItem(string itemSlug)
        {
            Item item = ItemDatabase.Instance.GetItem(itemSlug);
            if (AddToFirstEmptySlot(item))
            {
                return true;
            }
            else
            {
                Debug.Log("Inventory Full");
                return false;
            }
        }
        public bool GiveItem(Item item)
        {
            if (AddToFirstEmptySlot(item))
            {
                return true;
            }
            else
            {
                Debug.Log("Inventory Full");
                return false;
            }
        }
        public void RemoveItem(Item item)
        {
            for (int i = 0; i < equipmentList.Length; i++)
            {
                if(equipmentList[i].item == item)
                {
                    equipmentList[i].item = null;
                    return;
                }
            }
        }
        public void ConsumeItem(Item itemToConsume)
        {
            //consumableController.ConsumeItem(itemToConsume);
        }
        public InventorySlot[] GetEquipmentList
        {
            get
            {
                return equipmentList;
            }
        }
        public InventorySlot[] GetConsumablesList
        {
            get
            {
                return consumableList;
            }
        }
        public InventorySlot[] GetMaterialsList
        {
            get
            {
                return materialsList;
            }
        }
        public int ItemSpaceCount
        {
            get
            {
                return maxItemSpace;
            }
        }
        public int GetMoneyAcquired
        {
            get
            {
                return moneyAcquired;
            }
            set
            {
                moneyAcquired = value;
            }
        }
        public bool UseMoney(int charge, bool isSpending)
        {
            if (isSpending)
            {
                if(moneyAcquired <= charge)
                {
                    return false;
                }
                moneyAcquired -= charge;
            }
            else
            {
                moneyAcquired += charge;
            }
            return true;
        }

        private int GetSameItemIndex(Item item,List<InventorySlot> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].item.ItemId == item.ItemId)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}