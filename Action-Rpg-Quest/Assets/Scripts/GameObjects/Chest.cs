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
        private Sprite openSprite = null;
        private LootScript loot;
        private void Start()
        {
            isOpen = false;
            loot = GetComponent<LootScript>();
        }
        public void OpenChest()
        {
            if (!isOpen)
            {
                loot.DropLoot();
                isOpen = true;
                GetComponentInChildren<SpriteRenderer>().sprite = openSprite;
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                Debug.Log("already Opened");
            }
        }

        public void Interact()
        {
            OpenChest();
        }
    }
}
