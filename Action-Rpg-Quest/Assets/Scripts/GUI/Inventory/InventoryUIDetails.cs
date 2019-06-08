using Advent.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Advent.Entities;
using Advent.Enums;
using Advent.Manager;

namespace Advent.UI
{
    public class InventoryUIDetails : MonoBehaviour
    {
        Item item;
        Button selectedItemButton, itemInteractButton;
        TextMeshProUGUI itemNameText, itemDescriptionText, itemInteractButtonText;

        public TextMeshProUGUI statText;
        private void Start()
        {
            itemNameText = transform.Find("Item_Name").GetComponent<TextMeshProUGUI>();
            itemDescriptionText = transform.Find("Item_Description").GetComponent<TextMeshProUGUI>();
            itemInteractButton = transform.GetComponentInChildren<Button>();
            itemInteractButtonText = itemInteractButton.GetComponentInChildren<TextMeshProUGUI>();
            gameObject.SetActive(false);
        }
        public void SetItem(Item item, Button selectedButton)
        {
            gameObject.SetActive(true);
            statText.text = "";
            if(item.StatType != null)
            {
                foreach (BaseStat stat in item.StatType)
                {
                    statText.text += stat.statName + ": " + stat.baseValue + "\n";
                }
            }
            itemInteractButton.onClick.RemoveAllListeners();
            this.item = item;
            selectedItemButton = selectedButton;
            itemNameText.text = item.ItemName;
            itemDescriptionText.text = item.Description;
            itemInteractButtonText.text = item.ActionName;
            itemInteractButton.onClick.AddListener(OnItemInteract);
        }
        public void OnItemInteract()
        {
            if (item.ItemType == ItemTypes.CONSUMABLE)
            {
                InventoryManager.instance.ConsumeItem(item);
                Destroy(selectedItemButton.gameObject);
            }
            item = null;
            gameObject.SetActive(false);
        }
    }
}