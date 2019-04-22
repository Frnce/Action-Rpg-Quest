using Advent.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Manager
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }

        public int maxSpace = 20;
        public List<Item> items = new List<Item>();

        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallback;

        public bool AddItem(Item item)
        {
            if (!item.isDefaultItem)
            {
                if (items.Count >= maxSpace)
                {
                    Debug.Log("Not Enough Room");
                    return false;
                }
                items.Add(item);
                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();
                }
            }
            return true;
        }
        public void RemoveItem(Item item)
        {
            items.Remove(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }
    }
}