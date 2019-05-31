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
    }
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;
        [SerializeField]
        private int maxItemSpace = 50;
        [SerializeField]
        private int maxStack = 9;
        [SerializeField]
        private List<ItemsSpace> consumableList = new List<ItemsSpace>();
        [SerializeField]
        private List<ItemsSpace> equipmentList = new List<ItemsSpace>();
        [SerializeField]
        private List<ItemsSpace> enchantList = new List<ItemsSpace>();
        [SerializeField]
        private List<ItemsSpace> materialList = new List<ItemsSpace>();
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
        private bool CheckIfInventoryHasSpace(List<ItemsSpace> selectedList,int maxSpace)
        {
            if (selectedList.Count >= maxSpace)
            {
                Debug.Log("Not Enough Room");
                return false;
            }
            return true;
        }
        private void ConfirmedGivingItem(Item item,List<ItemsSpace> itemList)
        {
            int itemindex = GetSameItemIndex(item, itemList);
            if (item.isStackable && itemindex != -1 && itemList[itemindex].stackCount < maxStack)
            {
                itemList[itemindex].stackCount++;
            }
            else
            {
                itemList.Add(new ItemsSpace(item));
                UIEventHandlers.ItemAddedToInventory(item);
            }
        }
        public bool GiveItem(string itemSlug)
        {
            Item item = ItemDatabase.Instance.GetItem(itemSlug);
            if (item.ItemType == ItemTypes.CONSUMABLE) //CHeck if itemtype is consumable
            {
                if (CheckIfInventoryHasSpace(consumableList, maxItemSpace)) // check if consumable panel has empty slots
                {
                    ConfirmedGivingItem(item, consumableList); //give item (show on inventory)
                }
                else
                {
                    Debug.LogWarning("Unable to get item : full Inventory");
                    return false;
                }
            }
            else if(item.ItemType == ItemTypes.ENCHANTS)  //CHeck if itemtype is enchant
            {
                if (CheckIfInventoryHasSpace(enchantList, maxItemSpace))// check if enchant panel has empty slots
                {
                    ConfirmedGivingItem(item, enchantList);
                }
                else
                {
                    Debug.LogWarning("Unable to get item : full Inventory");
                    return false;
                }
            }
            else if(item.ItemType == ItemTypes.MATERIALS) //CHeck if itemtype is material
            {
                if (CheckIfInventoryHasSpace(materialList, maxItemSpace)) // check if material panel has empty slots
                {
                    ConfirmedGivingItem(item, materialList);
                }
                else
                {
                    Debug.LogWarning("Unable to get item : full Inventory");
                    return false;
                }
            }
            else   //Eqquipments itemtype
            {
                if (CheckIfInventoryHasSpace(equipmentList, maxItemSpace)) // check if equipment panel has empty slots
                {
                    ConfirmedGivingItem(item, equipmentList);
                }
                else
                {
                    Debug.LogWarning("Unable to get item : full Inventory");
                    return false;
                }
            }
            return true;
        }
        public bool GiveItem(Item item)
        {
            if (CheckIfInventoryHasSpace(consumableList, ItemSpaceCount))
            {
                int itemindex = GetSameItemIndex(item, consumableList);
                if (item.isStackable && itemindex != -1 && consumableList[itemindex].stackCount < maxStack)
                {
                    consumableList[itemindex].stackCount++;
                }
                else
                {
                    consumableList.Add(new ItemsSpace(item));
                    UIEventHandlers.ItemAddedToInventory(item);
                }
                return true;
            }
            else
            {
                Debug.LogWarning("Unable to get item : full Inventory");
                return false;
            }
        }
        public void RemoveItem(Item item)
        {
            int itemIndex = 0;
            itemIndex = GetSameItemIndex(item, consumableList);
            consumableList.Remove(consumableList[itemIndex]);

            //if (onItemChangedCallback != null)
            //{
            //    onItemChangedCallback.Invoke(item.itemType);
            //}
        }
        public void EquipItem(Item itemToEquip)
        {
            //playerWeaponController.EquipWeapon(itemToEquip);
        }

        public void ConsumeItem(Item itemToConsume)
        {
            //consumableController.ConsumeItem(itemToConsume);
        }

        public List<ItemsSpace> GetItems
        {
            get
            {
                return consumableList;
            }
            set
            {
                consumableList = value;
            }
        }
        public int ItemSpaceCount
        {
            get
            {
                return maxItemSpace;
            }
        }
        //public GameObject GetInventorySlotObject
        //{
        //    get
        //    {
        //        return inventorySlot;
        //    }
        //}

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