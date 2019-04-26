using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    public class LootScript : MonoBehaviour
    {
        [SerializeField]
        private LootTable lootTable = null;
        [SerializeField]
        private int dropChance = 0;
        public void DropLoot()
        {
            for (int i = 0; i < lootTable.GetMaxDrop; i++)
            {
                CalculateDropLoot();
            }
        }
        private void CalculateDropLoot()
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
                    itemWeight += lootTable.GetItems[i].dropChance;
                }
                Debug.Log("Item Weight : " + itemWeight);

                float randomValue = Random.Range(0, itemWeight);

                for (int i = 0; i < lootTable.GetItems.Count; i++)
                {
                    if(randomValue <= lootTable.GetItems[i].dropChance)
                    {
                        Vector2 itemPosition = (Random.insideUnitCircle * 2) + (Vector2)transform.position;
                        Instantiate(lootTable.GetItems[i].item, itemPosition, Quaternion.identity);
                        return;
                    }
                    randomValue -= lootTable.GetItems[i].dropChance;
                }
            }
        }
    }
}
