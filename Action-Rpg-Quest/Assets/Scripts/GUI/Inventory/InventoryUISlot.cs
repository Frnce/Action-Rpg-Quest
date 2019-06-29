using Advent.Items;
using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Advent.UI
{
    public enum SlotType
    {
        EQUIPMENT,
        INVENTORY_EQUIPMENT,
        INVENTORY_CONSUMABLES,
        INVENTORY_MATERIALS,
    }
    public class InventoryUISlot : MonoBehaviour,IDropHandler
    {
        public Item _item;
        public Image itemImage;

        public int Index { get; set; }
        public SlotType SlotType { get; set; }

        public Item Item
        {
            get => _item;
            set
            {
                SetItem(value);
            }
        }
        public void SetItem(Item item)
        {
            _item = item;
            if(item == null)
            {
                itemImage.enabled = false;
            }
            else
            {
                itemImage.enabled = true;

                Sprite[] sprites = Resources.LoadAll<Sprite>("Assets");
                for (int i = 0; i < sprites.Length; i++)
                {
                    if (sprites[i].name == Item.ObjectSlug)
                    {
                        itemImage.sprite = sprites[i];
                        break;
                    }
                }
            }
        }
        public void UnSetItem()
        {
            _item = null;
            itemImage.enabled = false;
            itemImage.sprite = null;
        }
        public void OnDrop(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<InventoryUIItem>();
            if(item != null)
            {
                if(item.InventoryParentSlot != null)
                {
                    if (item.InventoryParentSlot.Index == Index && item.InventoryParentSlot.SlotType == SlotType) return;

                    if (item.InventoryParentSlot.SlotType == SlotType.EQUIPMENT)
                    {

                    }
                    else if (item.InventoryParentSlot.SlotType == SlotType.INVENTORY_CONSUMABLES)
                    {
                        var sendingItem = InventoryManager.instance.PopItemFromnConsumablesSlot(item.InventoryParentSlot.Index);
                        var swappedItem = InventoryManager.instance.ReplaceItemInConsumablesSlot(sendingItem, Index);
                        InventoryManager.instance.ReplaceItemInConsumablesSlot(swappedItem, item.InventoryParentSlot.Index);
                    }
                    else if (item.InventoryParentSlot.SlotType == SlotType.INVENTORY_EQUIPMENT)
                    {
                        var sendingItem = InventoryManager.instance.PopItemFromEquipmentSlot(item.InventoryParentSlot.Index);
                        var swappedItem = InventoryManager.instance.ReplaceItemInEquipmentSlot(sendingItem, Index);
                        InventoryManager.instance.ReplaceItemInEquipmentSlot(swappedItem, item.InventoryParentSlot.Index);
                    }
                    else if(item.InventoryParentSlot.SlotType == SlotType.INVENTORY_MATERIALS)
                    {
                        var sendingItem = InventoryManager.instance.PopItemFromMaterialsSlot(item.InventoryParentSlot.Index);
                        var swappedItem = InventoryManager.instance.ReplaceItemInMaterialsSlot(sendingItem, Index);
                        InventoryManager.instance.ReplaceItemInConsumablesSlot(swappedItem, item.InventoryParentSlot.Index);
                    }
                }
                if(item.EquipmentParentSlot != null)
                {
                    if (item.EquipmentParentSlot.SlotType == SlotType) return;
                    //Unequip
                    EquipmentManager.instance.UnequipItem(item.EquipmentParentSlot.Item);
                    if (_item == null || _item.ItemType != item.EquipmentParentSlot.Item.ItemType)
                    {
                        InventoryManager.instance.ReplaceItemInEquipmentSlot(item.EquipmentParentSlot.Item, Index);
                    }
                }
            }
        }
        public void DiscardItem()
        {
            InventoryManager.instance.RemoveItem(Item);
            UnSetItem();
        }
    }
}