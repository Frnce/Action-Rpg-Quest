using Advent.Controller;
using Advent.Interfaces;
using Advent.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.GameObjects
{
    public enum ChestType
    {
        BASIC,
        GOLD
    }
    public class Chest : MonoBehaviour, IInteractable
    {
        protected bool isOpen = false;
        [SerializeField]
        protected bool isLocked = false;
        [SerializeField]
        protected ChestType chestType;
        [Space]
        [SerializeField]
        private LootTable lootTable = null;
        [SerializeField]
        [Range(0, 100)]
        private int lootDropChance = 0;
        [Space]
        [SerializeField]
        private Sprite openSprite = null;
        public LootDropTable DropTable { get; set; }
        public ItemPickup pickupItem;
        private void Start()
        {
            isOpen = false;
            DropTable = new LootDropTable();
            DropTable.loots = new List<LootDrop>
            {
                new LootDrop("Wpn_Short-Sword", 100)
            };
        }
        public void OpenChest()
        {
            if (!isOpen)
            {
                DropLoot();
                isOpen = true;
                GetComponentInChildren<SpriteRenderer>().sprite = openSprite;
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(gameObject);
            }
            else
            {   
                Debug.Log("already Opened");
            }
        }
        void DropLoot()
        {
            Item item = DropTable.GetDrop();
            if (item != null)
            {
                ItemPickup instance = Instantiate(pickupItem, transform.position, Quaternion.identity);
                instance.ItemDrop = item;
            }
        }
        public void Interact()
        {
            OpenChest();
        }
    }
}
