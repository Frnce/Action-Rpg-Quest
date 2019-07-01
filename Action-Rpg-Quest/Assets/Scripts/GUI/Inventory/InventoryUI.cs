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
        public RectTransform materialsContent;

        private InventoryManager inventoryManager;

        InventoryUISlot itemContainer { get; set; }
        List<InventoryUISlot> inventorySlotList = new List<InventoryUISlot>();
        bool MenuIsActive { get; set; }
        // Start is called before the first frame update
        void Start()
        {
            itemContainer = Resources.Load<InventoryUISlot>("GUI/Item_Container");
            inventoryPanel.gameObject.SetActive(false);
            MenuIsActive = false;
            inventoryManager = InventoryManager.instance;

            UIEventHandlers.OnInventoryUpdate += Redraw;
            Redraw();
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
        }
        private void DestroyAllSlots()
        {
            foreach (Transform child in equipmentContent.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in consumableContent.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in materialsContent.transform)
            {
                Destroy(child.gameObject);
            }
        }
        private void Redraw()
        {
            DestroyAllSlots();
            //Redraw Consumable Tab
            for (int i = 0; i < inventoryManager.GetConsumablesList.Length; i++)
            {
                InventoryUISlot emptyItem = Instantiate(itemContainer);
                emptyItem.transform.SetParent(consumableContent);
                emptyItem.SlotType = SlotType.INVENTORY_CONSUMABLES;
                emptyItem.Index = i;
                inventorySlotList.Add(emptyItem);

                emptyItem.SetItem(inventoryManager.GetConsumablesList[i].item);
            }
            //Redraw Materials Tab
            for (int i = 0; i < inventoryManager.GetMaterialsList.Length; i++)
            {
                InventoryUISlot emptyItem = Instantiate(itemContainer);
                emptyItem.transform.SetParent(materialsContent);
                emptyItem.SlotType = SlotType.INVENTORY_MATERIALS;
                emptyItem.Index = i;
                inventorySlotList.Add(emptyItem);

                emptyItem.SetItem(inventoryManager.GetMaterialsList[i].item);
            }
            //Redraw Equipment Tab
            for (int i = 0; i < inventoryManager.GetEquipmentList.Length; i++)
            {
                InventoryUISlot emptyItem = Instantiate(itemContainer);
                emptyItem.transform.SetParent(equipmentContent);
                emptyItem.SlotType = SlotType.INVENTORY_EQUIPMENT;
                emptyItem.Index = i;
                inventorySlotList.Add(emptyItem);

                emptyItem.SetItem(inventoryManager.GetEquipmentList[i].item);
            }
        }
    }
}