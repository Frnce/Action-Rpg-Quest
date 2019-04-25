using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    public class MoneyScript : MonoBehaviour
    {
        [SerializeField]
        private int moneyValue = 0;

        private InventoryManager inventoryManager = null;
        // Start is called before the first frame update
        void Start()
        {
            inventoryManager = InventoryManager.instance;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                inventoryManager.UseMoney(moneyValue,false);
                Destroy(gameObject);
            }
        }
    }
}
