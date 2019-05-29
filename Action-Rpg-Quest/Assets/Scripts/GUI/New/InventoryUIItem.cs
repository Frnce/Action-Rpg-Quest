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
        public TMPro.TextMeshProUGUI itemText;
        public Image itemImage;

        public void SetItem(Item item)
        {
            this.item = item;
            SetupItemValues();
        }

        private void SetupItemValues()
        {
            itemText.text = item.ItemName;
            itemImage.sprite = Resources.Load<Sprite>("UI/Icons/Item/" + item.ObjectSlug);
        }
        public void OnSelectItemButton()
        {
            InventoryManager.instance.SetItemDetails(item, GetComponent<Button>());
        }
    }
}