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
        public Item item;
        public Image itemImage;

        public int Index { get; set; }

        private Transform originalParent;

        private void Start()
        {
            originalParent = transform.parent;
        }

        public void SetItem(Item item)
        {
            this.item = item;
            SetupItemValues();
            itemImage.enabled = true;
        }

        private void SetupItemValues()
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Assets/MainTileset");
            for (int i = 0; i < sprites.Length; i++)
            {
                if(sprites[i].name == item.ObjectSlug)
                {
                    itemImage.sprite = sprites[i];
                    break;
                }
            }
        }
        public void OnSelectItemButton()
        {
            //InventoryManager.instance.SetItemDetails(item, GetComponent<Button>());
        }

        public void OnDrop(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<InventoryUIItem>();
            if (item.ParentSlot.Index == Index) return;

            var sendingItem = InventoryManager.instance.PopItemFromSlot(item.ParentSlot.Index);
            var swappedItem = InventoryManager.instance.ReplaceItemInSlot(sendingItem, Index);
            InventoryManager.instance.ReplaceItemInSlot(swappedItem, item.ParentSlot.Index);
        }

        public void DiscardItem()
        {
            InventoryManager.instance.RemoveItem(item);
        }
    }
}