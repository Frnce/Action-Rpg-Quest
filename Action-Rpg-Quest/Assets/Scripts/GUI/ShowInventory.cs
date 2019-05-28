using Advent.Controller;
using Advent.Entities;
using Advent.Enums;
using Advent.Items;
using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.UI
{
    public class ShowInventory : MonoBehaviour
    {
        [SerializeField]
        private Transform itemsPane = null;
        [SerializeField]
        private Transform equipmentsPane = null;
        [SerializeField]
        private Transform materialsPane = null;
        [SerializeField]
        private Transform enchantsPane = null;
        [SerializeField]
        private Transform etcPane = null;

        private InventoryManager inventory;
        private InventorySlot[] itemSlot;
        private InventorySlot[] equipmentSlot;
        private InventorySlot[] materialsSlot;
        private InventorySlot[] enchantSlot;
        private InventorySlot[] etcSlot;

        public GameObject inventoryParent;

        private bool inventoryPanelActive = false;
        // Start is called before the first frame update
        private void Awake()
        {
            inventory = InventoryManager.instance;
            inventory.onItemChangedCallback += UpdateInventoryUI;
        }
        void Start()
        {
            //Set Slots here
            SetItemSpace(inventory.ItemSpaceCount);
            SetEquipmentSpace(inventory.EquipmentSpaceCount);
            SetMaterialSpace(inventory.MaterialSpaceCount);
            SetEnchantSpace(inventory.EnchantSpaceCount);
            SetEtcSpace(inventory.EtcSpaceCount);

            itemSlot = itemsPane.GetComponentsInChildren<InventorySlot>();
            equipmentSlot = equipmentsPane.GetComponentsInChildren<InventorySlot>();
            materialsSlot = materialsPane.GetComponentsInChildren<InventorySlot>();
            enchantSlot = enchantsPane.GetComponentsInChildren<InventorySlot>();
            etcSlot = etcPane.GetComponentsInChildren<InventorySlot>();
        }
        //TEMPORARY
        private void Update()
        {
            if (PlayerController.instance.GetOpenMenuKey)
            {
                inventoryPanelActive = !inventoryPanelActive;
            }

            inventoryParent.gameObject.SetActive(inventoryPanelActive);

            if (inventoryParent.gameObject.activeSelf)
            {
                Player.instance.SetPlayerStates(PlayerStates.INMENU);
            }
        }
        private void AddToInventory(InventorySlot[] slot,List<ItemsSpace> listItem)
        {
            for (int i = 0; i < slot.Length; i++)
            {
                if (i < listItem.Count)
                {
                    slot[i].AddItem(listItem[i].item);
                }
                else
                {
                    slot[i].ClearSlot();
                }
            }
        }
        public void UpdateInventoryUI(ItemTypes itemType)
        {
            switch (itemType)
            {
                //case ItemTypes.CONSUMABLE:
                //    AddToInventory(itemSlot, inventory.GetItems);
                //    break;
                //case ItemTypes.EQUIPMENTS:
                //    AddToInventory(equipmentSlot,inventory.GetEquipments);
                //    break;
                //case ItemTypes.MATERIALS:
                //    AddToInventory(materialsSlot,inventory.GetMaterials);
                //    break;
                //case ItemTypes.ENCHANTS:
                //    AddToInventory(enchantSlot,inventory.GetMaterials);
                //    break;
                //case ItemTypes.ETC:
                //    AddToInventory(etcSlot,inventory.GetEtc);
                //    break;
                default:
                    break;
            }
        }
        public void SetItemSpace(int maxSpace)
        {
            for (int i = 0; i < maxSpace; i++)
            {
               var asd =  Instantiate(inventory.GetInventorySlotObject, itemsPane);
            }
        }
        public void SetEquipmentSpace(int maxSpace)
        {
            for (int i = 0; i < maxSpace; i++)
            {
                Instantiate(inventory.GetInventorySlotObject, equipmentsPane);
            }
        }
        public void SetMaterialSpace(int maxSpace)
        {
            for (int i = 0; i < maxSpace; i++)
            {
                Instantiate(inventory.GetInventorySlotObject, materialsPane);
            }
        }
        public void SetEnchantSpace(int maxSpace)
        {
            for (int i = 0; i < maxSpace; i++)
            {
                Instantiate(inventory.GetInventorySlotObject, enchantsPane);
            }
        }
        public void SetEtcSpace(int maxspace)
        {
            for (int i = 0; i < maxspace; i++)
            {
                Instantiate(inventory.GetInventorySlotObject, etcPane);
            }
        }
    }
}