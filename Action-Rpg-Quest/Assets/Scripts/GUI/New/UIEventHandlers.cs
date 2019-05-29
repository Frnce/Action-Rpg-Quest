using Advent.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.UI
{
    public class UIEventHandlers : MonoBehaviour
    {
        public delegate void ItemEvenhandler(Item item);
        public static event ItemEvenhandler OnItemAddedToInventory;
        public static event ItemEvenhandler OnItemEquipped;

        public delegate void PlayerHealthEventHandler(int currentHealth, int maxHealth);
        public static event PlayerHealthEventHandler OnPlayerHealthChanged;

        public delegate void StatsEventHandler();
        public static event StatsEventHandler OnStatsChanged;

        public delegate void PlayerLevelEventHandler();
        public static event PlayerLevelEventHandler OnPlayerLevelChange;

        public static void ItemAddedToInventory(Item item)
        {
            if (OnItemAddedToInventory != null)
            {
                OnItemAddedToInventory(item);
            }
        }
        public static void ItemAddedToInventory(List<Item> items)
        {
            if (OnItemAddedToInventory != null)
            {
                foreach (Item item in items)
                {
                    OnItemAddedToInventory(item);
                }
            }
        }
        public static void ItemEquipped(Item item)
        {
            if (OnItemEquipped != null)
                OnItemEquipped(item);
        }
        public static void HealthChanged(int currentHealth, int maxHealth)
        {
            if (OnPlayerHealthChanged != null)
                OnPlayerHealthChanged(currentHealth, maxHealth);
        }

        public static void StatsChanged()
        {
            if (OnStatsChanged != null)
                OnStatsChanged();
        }

        public static void PlayerLevelChanged()
        {
            if (OnPlayerLevelChange != null)
                OnPlayerLevelChange();
        }
    }
}