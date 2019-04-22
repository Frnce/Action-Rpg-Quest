using Advent.Entities;
using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    public class Item : ScriptableObject
    {
        new public string name = "New Item";
        public Sprite icon = null;
        public bool isDefaultItem = false;
        public GameObject gameobject;
        public int stackSize;

        public virtual void Use()
        {
            Debug.Log("Using " + name);
        }

        public void RemoveFromInventory()
        {
            InventoryManager.instance.RemoveItem(this);
        }
        public void DropFromInventory()
        {
            RemoveFromInventory();
            Instantiate(gameobject, Player.instance.transform.position, Quaternion.identity);
        }
    }
}