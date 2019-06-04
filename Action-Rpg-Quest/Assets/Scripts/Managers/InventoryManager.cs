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
    public class ItemsSpace
    {
        public Item item;
        public int stackCount = 1;

        public ItemsSpace(Item _item)
        {
            item = _item;
        }
        public ItemsSpace() { }
    }
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;
        [SerializeField]
        private int maxItemSpace = 50;
        [SerializeField]
        private int maxStack = 9;
        //[SerializeField]
        //private ItemsSpace[] consumableList;
        [SerializeField]
        private ItemsSpace[] equipmentList;
        //[SerializeField]
        //private List<ItemsSpace> enchantList = new List<ItemsSpace>();
        //[SerializeField]
        //private List<ItemsSpace> materialList = new List<ItemsSpace>();
        [Space]
        [SerializeField]
        private int moneyAcquired = 0;

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
        }
        private void Start()
        {
            equipmentList = new ItemsSpace[maxItemSpace];
            //Init item Slots
            for (int i = 0; i < equipmentList.Length; i++)
            {
                equipmentList[i] = new ItemsSpace();
            }
        }
        public bool AddToFirstEmptySlot(Item item)
        {
            for (int i = 0; i < equipmentList.Length; i++)
            {
                if (equipmentList[i].item == null)
                {
                    equipmentList[i].item = item;
                    UIEventHandlers.ItemAddedToInventory(item,i);
                    return true;
                }
            }
            return false;
        }
        public Item ReplaceItemInSlot(Item item,int slot)
        {
            var oldItem = equipmentList[slot].item;
            equipmentList[slot].item = item;
            return oldItem;
        }
        public Item PopItemFromSlot(int slot)
        {
            var item = equipmentList[slot].item;
            equipmentList[slot].item = null;
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
            //int itemIndex = 0;
            //itemIndex = GetSameItemIndex(item, consumableList);
            //consumableList.Remove(consumableList[itemIndex]);

            //if (onItemChangedCallback != null)
            //{
            //    onItemChangedCallback.Invoke(item.itemType);
            //}
            Debug.Log("Dropped " + item.ItemName);
        }
        public void EquipItem(Item itemToEquip)
        {
            //playerWeaponController.EquipWeapon(itemToEquip);
        }

        public void ConsumeItem(Item itemToConsume)
        {
            //consumableController.ConsumeItem(itemToConsume);
        }

        //public List<ItemsSpace> GetItems
        //{
        //    get
        //    {
        //        return consumableList;
        //    }
        //    set
        //    {
        //        consumableList = value;
        //    }
        //}
        public ItemsSpace[] GetEquipmentList
        {
            get
            {
                return equipmentList;
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

        private int GetSameItemIndex(Item item,List<ItemsSpace> list)
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