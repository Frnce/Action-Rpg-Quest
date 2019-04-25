using Advent.Controller;
using Advent.Interfaces;
using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    public class ItemPickup : MonoBehaviour,IInteractable
    {
        public Item item;

        private void Pickup()
        {
            bool isPickedUp = InventoryManager.instance.AddItem(item);

            if (isPickedUp)
            {
                Destroy(gameObject);
            }
        }

        public void Interact()
        {
            Pickup();
        }
    }
}
