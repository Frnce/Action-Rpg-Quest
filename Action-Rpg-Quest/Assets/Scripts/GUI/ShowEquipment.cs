﻿using Advent.Controller;
using Advent.Entities;
using Advent.Items;
using Advent.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.UI
{
    public class ShowEquipment : MonoBehaviour
    {
        [SerializeField] private Transform equipmentParent = null;

        private EquipmentManager equipmentManager;
        private EquipmentSlot[] equipmentSlot;

        private void Awake()
        {
            equipmentManager = EquipmentManager.instance;
            equipmentManager.onEquipmentChangedCallback += UpdateEquipmentUI;
        }
        private void UpdateEquipmentUI(Equipment newItem, Equipment oldItem)
        {
            if(equipmentSlot == null)
            {
                equipmentSlot = equipmentParent.GetComponentsInChildren<EquipmentSlot>();
            }
            if (newItem != null)
            {
                int slotIndex = (int)newItem.GetSlots;
                for (int i = 0; i < equipmentSlot.Length; i++)
                {
                    if (i < equipmentManager.currentEquipment.Length)
                    {
                        equipmentSlot[slotIndex].AddItem(newItem, oldItem);
                    }
                    else
                    {
                        equipmentSlot[i].ClearSlot();
                    }
                }
            }
        }
    }
}
