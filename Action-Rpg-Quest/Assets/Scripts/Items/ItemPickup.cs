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
        public Item ItemDrop { get; set; }
        private void Pickup()
        {
            bool isPickedUp = InventoryManager.instance.GiveItem(ItemDrop);

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
