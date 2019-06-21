using Advent.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.UI
{
    public class UIEventHandlers : MonoBehaviour
    {
        public delegate void ItemEvenhandler(Item item,int slotIndex);
        public static event ItemEvenhandler OnItemAddedToInventory;
        public static event ItemEvenhandler OnItemEquipped;

        public delegate void ItemEventHandler();
        public static event ItemEventHandler OnInventoryUpdate;

        public delegate void EquipEventHandler();
        public static event EquipEventHandler OnEquipUpdate;

        public delegate void PlayerHealthEventHandler(int currentHealth, int maxHealth);
        public static event PlayerHealthEventHandler OnPlayerHealthChanged;

        public delegate void StatsEventHandler();
        public static event StatsEventHandler OnStatsChanged;

        public delegate void PlayerLevelEventHandler();
        public static event PlayerLevelEventHandler OnPlayerLevelChange;

        public static void ItemAddedToInventory(Item item,int slotIndex)
        {
            if(OnItemAddedToInventory != null)
            {
                OnItemAddedToInventory.Invoke(item,slotIndex);
            }
        }
        public static void InventoryUpdate()
        {
            if(OnInventoryUpdate != null)
            {
                OnInventoryUpdate.Invoke();
            }
        }
        public static void EquipUpdate()
        {
            if(OnEquipUpdate != null)
            {
                OnEquipUpdate.Invoke();
            }
        }
        public static void ItemEquipped(Item item,int slotIndex)
        {
            if (OnItemEquipped != null)
                OnItemEquipped(item,slotIndex);
        }
        public static void HealthChanged(int currentHealth, int maxHealth)
        {
            if (OnPlayerHealthChanged != null)
                OnPlayerHealthChanged(currentHealth, maxHealth);
        }

        public static void StatsChanged()
        {
            if (OnStatsChanged != null)
            {
                OnStatsChanged.Invoke();
            }
        }

        public static void PlayerLevelChanged()
        {
            if (OnPlayerLevelChange != null)
                OnPlayerLevelChange();
        }
    }
}