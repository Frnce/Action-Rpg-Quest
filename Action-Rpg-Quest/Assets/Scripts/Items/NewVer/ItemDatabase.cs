using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items.New
{
    public class ItemDatabase : MonoBehaviour
    {
        public static ItemDatabase Instance { get; set; }
        private List<Item> Items { get; set; }
        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
            BuildDatabase();
        }
        private void BuildDatabase()
        {
            Items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("JSON/Items").ToString());
        }
        public Item GetItem(string itemSlug)
        {
            foreach (Item item in Items)
            {
                if(item.ObjectSlug == itemSlug)
                {
                    return item;
                }
            }
            Debug.LogWarning("Couldn't find Item : " + itemSlug);
            return null;
        }
    }
}