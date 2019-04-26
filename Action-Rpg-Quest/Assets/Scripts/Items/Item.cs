using Advent.Entities;
using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    public class Item : ScriptableObject
    {
        public int id = 0;
        new public string name = "New Item";
        public Sprite icon = null;
        public bool isDefaultItem = false;
        public GameObject gameobject;
        public int stackSize;         //stacksize -1 = not stackable
        public int dropRate;

        public virtual void Use()
        {
            Debug.Log("Using " + name);
            InventoryManager inventory = InventoryManager.instance;
            for (int i = 0; i < inventory.GetItems.Count; i++)
            {
                if(inventory.GetItems[i].id == id)
                {
                    if(inventory.GetItems[i].stack <= 1)
                    {
                        RemoveFromInventory();
                        break;
                    }
                    inventory.GetItems[i].stack--;
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