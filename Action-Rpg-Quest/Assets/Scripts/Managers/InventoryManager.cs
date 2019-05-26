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
        public ItemTypes itemType;

        public ItemsSpace(int _id,int _stack,Item _item,ItemTypes _itemType)
        {
            id = _id;
            stack = _stack;
            item = _item;
            itemType = _itemType;
        }

        public ItemsSpace(int _stack)
        {
            stack = _stack;
        }
    }
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;
        [SerializeField]
        private int maxItemSpace = 20;
        [SerializeField]
        private int maxEquipmentSpace = 20;
        [SerializeField]
        private int maxMaterialSpace = 20;
        [SerializeField]
        private int maxEnchantSpace = 20; //RUNE = Placeholder name for equipment enchants
        [SerializeField]
        private int maxEtcSpace = 20; //RUNE = Placeholder name for equipment enchants
        [SerializeField]
        private int maxStack = 9;
        [SerializeField]
        private List<ItemsSpace> itemList = new List<ItemsSpace>();
        [SerializeField]
        private List<ItemsSpace> equipmentList = new List<ItemsSpace>();
        [SerializeField]
        private List<ItemsSpace> materialList = new List<ItemsSpace>();
        [SerializeField]
        private List<ItemsSpace> enchantList = new List<ItemsSpace>();
        [SerializeField]
        private List<ItemsSpace> etcList = new List<ItemsSpace>();
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

            int itemIndex = GetSameItemIndex(item,listItems);

            if (itemIndex != -1 && listItems[itemIndex].stack != -1 && listItems[itemIndex].stack < maxStack)
            {
                listItems[itemIndex].stack++;
            }
            else
            {
                listItems.Add(new ItemsSpace(item.itemId, item.stackSize, item, item.itemType));
            }

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke(item.itemType);
            }
        }
        public bool AddItem(Item item)
        {
            switch (item.itemType)
            {
                case ItemTypes.CONSUMABLE:
                    if (!CheckIfInventoryHasSpace(itemList, maxItemSpace))
                    {
                        return false;
                    }
                    else
                    {
                        AddItemToPane(item, itemList);
                    }
                    break;
                case ItemTypes.EQUIPMENTS:
                    if (!CheckIfInventoryHasSpace(equipmentList, maxEquipmentSpace))
                    {
                        return false;
                    }
                    else
                    {
                        AddItemToPane(item, equipmentList);
                    }
                    break;
                case ItemTypes.MATERIALS:
                    if (!CheckIfInventoryHasSpace(materialList, maxMaterialSpace))
                    {
                        return false;
                    }
                    else
                    {
                        AddItemToPane(item, materialList);
                    }
                    break;
                case ItemTypes.ENCHANTS:
                    if (!CheckIfInventoryHasSpace(enchantList, maxEnchantSpace))
                    {
                        return false;
                    }
                    else
                    {
                        AddItemToPane(item, enchantList);
                    }
                    break;
                case ItemTypes.ETC:
                    if (!CheckIfInventoryHasSpace(etcList, maxEtcSpace))
                    {
                        return false;
                    }
                    else
                    {
                        AddItemToPane(item, etcList);
                    }
                    break;
                default:
                    break;
            }
            return true;
        }
        public void RemoveItem(Item item)
        {
            int itemIndex = 0;
            switch (item.itemType)
            {
                case ItemTypes.CONSUMABLE:
                    itemIndex = GetSameItemIndex(item,itemList);
                    itemList.Remove(itemList[itemIndex]);
                    break;
                case ItemTypes.EQUIPMENTS:
                    itemIndex = GetSameItemIndex(item,equipmentList);
                    equipmentList.Remove(equipmentList[itemIndex]);
                    break;
                case ItemTypes.MATERIALS:
                    itemIndex = GetSameItemIndex(item,materialList);
                    materialList.Remove(materialList[itemIndex]);
                    break;
                case ItemTypes.ENCHANTS:
                    itemIndex = GetSameItemIndex(item,enchantList);
                    enchantList.Remove(enchantList[itemIndex]);
                    break;
                case ItemTypes.ETC:
                    itemIndex = GetSameItemIndex(item,etcList);
                    etcList.Remove(etcList[itemIndex]);
                    break;
                default:
                    break;
            }
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke(item.itemType);
            }
        }

        public List<ItemsSpace> GetItems
        {
            get
            {
                return itemList;
            }
            set
            {
                itemList = value;
            }
        }
        public List<ItemsSpace> GetEquipments
        {
            get
            {
                return equipmentList;
            }
            set
            {
                equipmentList = value;
            }
        }
        public List<ItemsSpace> GetMaterials
        {
            get
            {
                return materialList;
            }
            set
            {
                materialList = value;
            }
        }
        public List<ItemsSpace> GetEnchants
        {
            get
            {
                return enchantList;
            }
            set
            {
                enchantList = value;
            }
        }
        public List<ItemsSpace> GetEtc
        {
            get
            {
                return etcList;
            }
            set
            {
                etcList = value;
            }
        }
        public int ItemSpaceCount
        {
            get
            {
                return maxItemSpace;
            }
        }
        public int EquipmentSpaceCount
        {
            get
            {
                return maxEquipmentSpace;
            }
        }
        public int MaterialSpaceCount
        {
            get
            {
                return maxMaterialSpace;
            }
        }
        public int EnchantSpaceCount
        {
            get
            {
                return maxEnchantSpace;
            }
        }
        public int EtcSpaceCount
        {
            get
            {
                return maxEtcSpace;
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
                if(list[i].id == item.itemId)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}