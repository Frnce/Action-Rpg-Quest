using Advent.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.UI
{
    public class InventoryUI : MonoBehaviour
    {
        public RectTransform inventoryPanel;
        public RectTransform scrollViewContent;

        InventoryUIItem itemContainer { get; set; }
        List<InventoryUIItem> itemUIList = new List<InventoryUIItem>();
        bool menuIsActive { get; set; }
        Item currentSelectedItem { get; set; }
        // Start is called before the first frame update
        void Start()
        {
            itemContainer = Resources.Load<InventoryUIItem>("GUI/Item_Container");
            inventoryPanel.gameObject.SetActive(false);

            UIEventHandlers.OnItemAddedToInventory += ItemAdded;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                inventoryPanel.gameObject.SetActive(true);
            }
        }

        public void ItemAdded(Item item)
        {
            InventoryUIItem emptyItem = Instantiate(itemContainer);
            emptyItem.SetItem(item);
            itemUIList.Add(emptyItem);
            emptyItem.transform.SetParent(scrollViewContent);
        }
    }
}