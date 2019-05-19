using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    [System.Serializable]
    public struct Loot
    {
        public GameObject item;
        [Range(0,100)]
        public float dropRate;
    }
    [CreateAssetMenu(fileName = "New Loot Table", menuName = "Loot Table")]
    public class LootTable : ScriptableObject
    {
        [SerializeField]
        private List<Loot> items = new List<Loot>();
        [SerializeField]
        private int maxDrop = 0;

        public List<Loot> GetItems
        {
            get
            {
                return items;
            }
        }
        public int GetMaxDrop
        {
            get
            {
                return maxDrop;
            }
        }
    }
}