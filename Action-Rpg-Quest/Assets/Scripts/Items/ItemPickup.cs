using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    public class ItemPickup : MonoBehaviour
    {
        public Item item;
        private bool isStepOn = false; //Checks if Player is inside the tile of the item

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && isStepOn) //TODO Change input
            {
                Pickup();
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isStepOn = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isStepOn = false;
            }
        }
        void Pickup()
        {
            bool isPickedUp = InventoryManager.instance.AddItem(item);

            if (isPickedUp)
            {
                Destroy(gameObject);
            }
        }
    }
}
