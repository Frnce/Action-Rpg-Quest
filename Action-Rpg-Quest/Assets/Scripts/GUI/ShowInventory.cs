using Advent.Controller;
using Advent.Entities;
using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.UI
{
    public class ShowInventory : MonoBehaviour
    {
        [SerializeField]
        private Transform inventoryParent = null;

        private InventoryManager inventory;
        private InventorySlot[] itemSlot;

        private bool inventoryPanelActive = false;
        // Start is called before the first frame update
        void Start()
        {
            inventory = InventoryManager.instance;
            inventory.onItemChangedCallback += UpdateInventoryUI;

            SetInventorySpace(inventory.GetMaxBagSpace);
            SetPocketSpace(inventory.GetPocketItemSpace);
            itemSlot = inventoryParent.GetComponentsInChildren<InventorySlot>();

            inventoryParent.gameObject.SetActive(false);
        }
        //TEMPORARY
        private void Update()
        {
            if (PlayerController.instance.GetOpenMenuKey)
            {
                inventoryPanelActive = !inventoryPanelActive;
            }

            inventoryParent.gameObject.SetActive(inventoryPanelActive);
        }
        public void UpdateInventoryUI()
        {
            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (i < inventory.GetItems.Count)
                {
                    itemSlot[i].AddItem(inventory.GetItems[i].item);
                }
                else
                {
                    itemSlot[i].ClearSlot();
                }
            }
        }
        public void SetInventorySpace(int maxSpace)
        {
            for (int i = 0; i < maxSpace; i++)
            {
                Instantiate(inventory.GetInventorySlotObject, inventoryParent);
            }
        }
        public void SetPocketSpace(int maxSpace)
        {
            for (int i = 0; i < maxSpace; i++)
            {
                //TODO Fix This
            }
        }
    }
}