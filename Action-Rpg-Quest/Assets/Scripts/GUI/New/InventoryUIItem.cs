using Advent.Items;
using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Advent.UI
{
    public class InventoryUIItem : MonoBehaviour
    {
        public Item item;
        public Image itemImage;

        public void SetItem(Item item)
        {
            this.item = item;
            SetupItemValues();
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
    }
}