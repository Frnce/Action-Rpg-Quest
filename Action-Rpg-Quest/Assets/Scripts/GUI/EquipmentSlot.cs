using Advent.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Advent.UI
{
    public class EquipmentSlot : MonoBehaviour
    {
        private Equipment equipment;
        [SerializeField] private Image icon = null;
        [SerializeField] private int thisIndex = 0;

        public void AddItem(Equipment newItem, Equipment oldItem)
        {
            equipment = newItem;

            icon.sprite = newItem.icon;
            icon.enabled = true;
        }
        public void ClearSlot()
        {
            equipment = null;
            icon.sprite = null;
            icon.enabled = false;
        }
        public void UseItem()
        {
            if (equipment != null)
            {
                equipment.Use();
            }
        }
        public void UnequipItem()
        {
            if (equipment != null)
            {
                equipment.Unequip(thisIndex);
                ClearSlot();
            }
        }
    }
}