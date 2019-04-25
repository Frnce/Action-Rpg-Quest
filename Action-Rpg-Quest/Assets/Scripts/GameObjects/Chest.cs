using Advent.Controller;
using Advent.Interfaces;
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
        protected GameObject[] items;
        [SerializeField]
        protected ChestType chestType;
        [Space]
        [SerializeField]
        private Sprite openSprite = null;

        private void Start()
        {
            isOpen = false;
        }
        public void OpenChest()
        {
            if (!isOpen)
            {
                foreach (GameObject item in items)
                {
                    Vector2 itemPosition = (Random.insideUnitCircle*2) + (Vector2)transform.position;
                    Debug.Log(itemPosition);
                    Instantiate(item, itemPosition, Quaternion.identity);
                }
                isOpen = true;
                GetComponentInChildren<SpriteRenderer>().sprite = openSprite;
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
