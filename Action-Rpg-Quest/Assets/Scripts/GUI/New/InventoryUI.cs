using Advent.Controller;
using Advent.Entities;
using Advent.Items;
using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.UI
{
    public class InventoryUI : MonoBehaviour
    {
        public RectTransform inventoryPanel;
        [Space]
        public RectTransform consumableContent;
        public RectTransform equipmentContent;
        public RectTransform enchantsContent;
        public RectTransform materialsContent;

        private InventoryManager inventoryManager;

        InventoryUISlot itemContainer { get; set; }
        List<InventoryUISlot> inventorySlotList = new List<InventoryUISlot>();
        bool MenuIsActive { get; set; }
        Item currentSelectedItem { get; set; }
        // Start is called before the first frame update
        void Start()
        {
            itemContainer = Resources.Load<InventoryUISlot>("GUI/Item_Container");
            inventoryPanel.gameObject.SetActive(false);
            MenuIsActive = false;
            inventoryManager = InventoryManager.instance;

            InitItemSlots();

            UIEventHandlers.OnItemAddedToInventory += ItemAdded;
        }

        // Update is called once per frame
        void Update()
        {
            if (PlayerController.instance.GetShowInventoryKey)
            {
                MenuIsActive = !MenuIsActive;
            }

            if (MenuIsActive)
            {
                Player.instance.SetPlayerStates(PlayerStates.INMENU);
                CameraController.instance.EnableDisableCameraControls(false);
            }
            else
            {
                Player.instance.SetPlayerStates(PlayerStates.IDLE);
                CameraController.instance.EnableDisableCameraControls(true);
            }
            inventoryPanel.gameObject.SetActive(MenuIsActive);
        }
        public void ItemAdded(Item item,int index)
        {
            inventorySlotList[index].SetItem(item);
            //InventoryUISlot emptyItem = Instantiate(itemContainer);
            //emptyItem.SetItem(item);
            //itemUIList.Add(emptyItem);
            //if(item.ItemType == Enums.ItemTypes.CONSUMABLE)
            //{
            //    emptyItem.transform.SetParent(consumableContent);
            //}
            //else if (item.ItemType == Enums.ItemTypes.MATERIALS)
            //{
            //    emptyItem.transform.SetParent(materialsContent);
            //}
            //else if(item.ItemType == Enums.ItemTypes.ENCHANTS)
            //{
            //    emptyItem.transform.SetParent(enchantsContent);
            //}
            //else //Equipments
            //{
            //    emptyItem.transform.SetParent(equipmentContent);
            //}
        }
        public void InitItemSlots()
        {
            for (int i = 0; i < inventoryManager.GetEquipmentList.Length; i++)
            {
                InventoryUISlot emptyItem = Instantiate(itemContainer);
                emptyItem.transform.SetParent(equipmentContent);
                emptyItem.Index = i;
                inventorySlotList.Add(emptyItem);
            }
        }
    }
}