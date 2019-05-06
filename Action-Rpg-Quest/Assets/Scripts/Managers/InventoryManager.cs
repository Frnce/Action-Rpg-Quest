using Advent.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Manager
{
    [System.Serializable]
    public class ItemsSpace
    {
        public int id;
        public int stack;
        public Item item;

        public ItemsSpace(int _id,int _stack,Item _item)
        {
            id = _id;
            stack = _stack;
            item = _item;
        }

        public ItemsSpace(int _stack)
        {
            stack = _stack;
        }
    }
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;
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
        [SerializeField]
        private int maxPocketSpace = 5;
        [SerializeField]
        private int maxBagSpace = 20;
        [SerializeField]
        private int maxStack = 9;
        [SerializeField]
        private List<ItemsSpace> items = new List<ItemsSpace>();
        [Space]
        [SerializeField]
        private int moneyAcquired = 0;
        [Space]
        [SerializeField]
        private GameObject inventorySlot = null;

        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallback;

        public bool AddItem(Item item)
        {
            if (!item.isDefaultItem)
            {
                if (items.Count >= maxPocketSpace && items.Count >= maxBagSpace)
                {
                    Debug.Log("Not Enough Room");
                    return false;
                }

                int itemIndex = GetSameItemIndex(item);

                if (itemIndex != -1 && items[itemIndex].stack != -1 && items[itemIndex].stack < maxStack)
                {
                    items[itemIndex].stack++;
                }
                else
                {
                    items.Add(new ItemsSpace(item.id, item.stackSize, item));
                }

                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();
                }
            }
            return true;
        }
        public void RemoveItem(Item item)
        {
            int itemIndex = GetSameItemIndex(item);
            items.Remove(items[itemIndex]);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }

        public List<ItemsSpace> GetItems
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
        }

        public int GetMaxBagSpace
        {
            get
            {
                return maxBagSpace;
            }
        }
        public int GetPocketItemSpace
        {
            get
            {
                return maxPocketSpace;
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
        private int GetSameItemIndex(Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].id == item.id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}