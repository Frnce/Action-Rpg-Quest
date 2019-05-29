using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    public class LootDropTable
    {
        public List<LootDrop> loots;

        public Item GetDrop()
        {
            int roll = Random.Range(0, 101);
            int weightSum = 0;
            foreach (LootDrop drop in loots)
            {
                weightSum += drop.Weight;
                if(roll < weightSum)
                {
                    return ItemDatabase.Instance.GetItem(drop.ItemSlug);
                }
            }
            return null;
        }
    }

    public class LootDrop
    {
        public string ItemSlug { get; set; }
        public int Weight { get; set; }

        public LootDrop(string itemSlug, int weight)
        {
            ItemSlug = itemSlug;
            Weight = weight;
        }
    }
}