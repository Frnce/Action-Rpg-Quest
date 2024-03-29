﻿using Advent.Enums;
using Advent.Items;
using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Advent.UI
{
    public class EquipmentUISlot : MonoBehaviour,IDropHandler
    {
        public Item _item;
        public Image itemImage;

        public SlotType SlotType { get; set; }
        public EquipTypes EquipType { get; set; }

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
            if (item == null)
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

        public void DiscardItem()
        {
            //InventoryManager.instance.RemoveItem(Item);
            UnSetItem();
        }

        public void OnDrop(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<InventoryUIItem>();

            if (item != null)
            {
                if(item.InventoryParentSlot != null)
                {
                    if (item.InventoryParentSlot.Item.EquipType != EquipType)
                    {
                        return;
                    }
                    if (_item != null) // When The Equipment slot is not empty - Proceed to Swap it
                    {
                        //Swap the selected Equipment slot from the equipment Manager;
                        EquipmentManager.instance.SwapItem(item.InventoryParentSlot.Item, Item);
                        //return the old item to inventory;
                        InventoryManager.instance.AddToFirstEmptySlot(Item);
                    }
                    else
                    {
                        EquipmentManager.instance.EquipItem(item.InventoryParentSlot.Item);
                    }
                    item.InventoryParentSlot.UnSetItem();
                }
            }
        }
    }
}