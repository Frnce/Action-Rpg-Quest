using Advent.Entities;
using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    public enum ItemTypes
    {
        CONSUMABLE,
        EQUIPMENTS,
        MATERIALS,
        ENCHANTS,
        ETC
    }
    public class Item : ScriptableObject
    {
        [Header("Item Data")]
        public int itemId = 0;
        new public string name = "New Item";
        public ItemTypes itemType;
        [Space]
        public Sprite icon = null;
        public GameObject gameobject;
        [Space]
        public int stackSize;         //stacksize -1 = not stackable
        public int dropRate;

        private List<ItemsSpace> GetItemList()
        {
            switch (itemType)
            {
                case ItemTypes.CONSUMABLE:
                    return InventoryManager.instance.GetItems;
                case ItemTypes.EQUIPMENTS:
                    return InventoryManager.instance.GetEquipments;
                case ItemTypes.MATERIALS:
                    return InventoryManager.instance.GetMaterials;
                case ItemTypes.ENCHANTS:
                    return InventoryManager.instance.GetEnchants;
                case ItemTypes.ETC:
                    return InventoryManager.instance.GetEtc;
                default:
                    return null;
            }
        }
        public virtual void Use()
        {
            Debug.Log("Using " + name);
            for (int i = 0; i < GetItemList().Count; i++)
            {
                if(GetItemList()[i].id == itemId)
                {
                    if(GetItemList()[i].stack <= 1)
                    {
                        RemoveFromInventory();
                        break;
                    }
                    GetItemList()[i].stack--;
                    break;
                }
            }
        }

        public void RemoveFromInventory()
        {
            //if stack is -1 or less than 1
            InventoryManager.instance.RemoveItem(this);
        }
        public void DropFromInventory()
        {
            RemoveFromInventory();
            Instantiate(gameobject, Player.instance.transform.position, Quaternion.identity);
        }
    }
}