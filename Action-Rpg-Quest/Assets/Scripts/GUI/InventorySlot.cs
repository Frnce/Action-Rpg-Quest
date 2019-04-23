using Advent.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Advent.UI
{
    public class InventorySlot : MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        Item item;
        public Image panel;
        public Image icon;

        public void AddItem(Item newItem)
        {
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;
        }
        public void ClearSlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
        }

        public void UseItem()
        {
            if (item != null)
            {
                item.Use();
            }
        }
        public void DropItem()
        {
            if (item != null)
            {
                item.DropFromInventory();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            panel.color = Color.blue;
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                //Left mouse button
                UseItem();
            }
            else if (eventData.button == PointerEventData.InputButton.Middle)
            {
                //Middle Mouse Button
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                //Right Mouse Button
                DropItem();
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            panel.color = Color.cyan;
            //Add Item Information on UI
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            panel.color = Color.white;
            //Remove item info on UI
        }
    }
}
