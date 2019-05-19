using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    public class LootScript : MonoBehaviour
    {
        public void DropLoot(LootTable lootTable,int dropChance,Vector3 dropPosition)
        {
            for (int i = 0; i < lootTable.GetMaxDrop; i++)
            {
                CalculateDropLoot(lootTable,dropChance,dropPosition);
            }
        }
        private void CalculateDropLoot(LootTable lootTable,int dropChance,Vector3 dropPosition)
        {
            int calculateDropChance = Random.Range(0, 101);

            if(calculateDropChance > dropChance)
            {
                Debug.Log("No Loot for you");
                return;
            }
            if(calculateDropChance <= dropChance)
            {
                float itemWeight = 0;

                for (int i = 0; i < lootTable.GetItems.Count; i++)
                {
                    itemWeight += lootTable.GetItems[i].dropRate;
                }
                Debug.Log("Item Weight : " + itemWeight);

                float randomValue = Random.Range(0, itemWeight);

                for (int i = 0; i < lootTable.GetItems.Count; i++)
                {
                    if(randomValue <= lootTable.GetItems[i].dropRate)
                    {
                        Vector2 itemPosition = (Random.insideUnitCircle * 2) + (Vector2)dropPosition;
                        Instantiate(lootTable.GetItems[i].item, itemPosition, Quaternion.identity);
                        return;
                    }
                    randomValue -= lootTable.GetItems[i].dropRate;
                }
            }
        }
    }
}
