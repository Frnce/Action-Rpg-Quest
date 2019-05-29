using Advent.Enums;
using Advent.Items  ;
using Advent.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        private int maxItemSpace = 20;
        [SerializeField]
        private int maxStack = 9;
        [SerializeField]
        private List<ItemsSpace> itemsList = new List<ItemsSpace>();
        [Space]
        [SerializeField]
        private int moneyAcquired = 0;
        [Space]
        [SerializeField]
        private GameObject inventorySlot = null;

        public delegate void OnItemChanged(ItemTypes itemType);
        public OnItemChanged onItemChangedCallback;

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
        private void AddItemToPane(Item item,List<ItemsSpace> listItems)
        {
            //if (onItemChangedCallback != null)
            //{
            //    onItemChangedCallback.Invoke(item.itemType);
            //}
        }
        public bool GiveItem(string itemSlug)
        {
            if (CheckIfInventoryHasSpace(itemsList, ItemSpaceCount))
            {
                Item item = ItemDatabase.Instance.GetItem(itemSlug);
                int itemindex = GetSameItemIndex(item, itemsList);
                if(item.isStackable && itemindex != -1 && itemsList[itemindex].stackCount < maxStack)
                {
                    itemsList[itemindex].stackCount++;
                }
                else
                {
                    itemsList.Add(new ItemsSpace(item));
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
        public bool GiveItem(Item item)
        {
            if (CheckIfInventoryHasSpace(itemsList, ItemSpaceCount))
            {
                int itemindex = GetSameItemIndex(item, itemsList);
                if (item.isStackable && itemindex != -1 && itemsList[itemindex].stackCount < maxStack)
                {
                    itemsList[itemindex].stackCount++;
                }
                else
                {
                    itemsList.Add(new ItemsSpace(item));
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
            itemIndex = GetSameItemIndex(item, itemsList);
            itemsList.Remove(itemsList[itemIndex]);

            //if (onItemChangedCallback != null)
            //{
            //    onItemChangedCallback.Invoke(item.itemType);
            //}
        }

        public List<ItemsSpace> GetItems
        {
            get
            {
                return itemsList;
            }
            set
            {
                itemsList = value;
            }
        }
        public int ItemSpaceCount
        {
            get
            {
                return maxItemSpace;
            }
        }
        public GameObject GetInventorySlotObject
        {
            get
            {
                return inventorySlot;
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