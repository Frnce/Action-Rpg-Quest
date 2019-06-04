using Advent.Items;
using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Advent.UI
{
    public class InventoryUISlot : MonoBehaviour,IDropHandler
    {
        public Item _item;
        public Image itemImage;

        public int Index { get; set; }

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

                Sprite[] sprites = Resources.LoadAll<Sprite>("Assets/MainTileset");
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
        public void OnDrop(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<InventoryUIItem>();
            if(item != null)
            {
                if (item.ParentSlot.Index == Index) return;

                var sendingItem = InventoryManager.instance.PopItemFromSlot(item.ParentSlot.Index);
                var swappedItem = InventoryManager.instance.ReplaceItemInSlot(sendingItem, Index);
                InventoryManager.instance.ReplaceItemInSlot(swappedItem, item.ParentSlot.Index);
            }
        }
        public void DiscardItem()
        {
            InventoryManager.instance.RemoveItem(Item);
        }
    }
}