using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(menuName = "Items/Consumables/HealthPotion", fileName = "New Health Potion")]
    public class HealthPotion : Consumable
    {
        [Header("Item Specific")]
        public int HealAmount = 0;

        public override void Use()
        {
            base.Use();
        }
    }
}