using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Advent.Manager;

namespace Advent.UI
{
    public class EquipmentUI : MonoBehaviour
    {
        public RectTransform EquipmentPanel;
        [Space]
        private EquipmentManager equipmentManager;
        private EquipmentUISlot equipmentContainer;

        List<EquipmentUISlot> equipmentSlotList = new List<EquipmentUISlot>();
        // Use this for initialization
        void Start()
        {
            equipmentContainer = Resources.Load<EquipmentUISlot>("GUI/Equipment_Container");
            equipmentManager = EquipmentManager.instance;

            UIEventHandlers.OnEquipUpdate += Redraw;
            Redraw();
        }

        private void Redraw()
        {
            foreach (Transform child in EquipmentPanel.transform)
            {
                Destroy(child.gameObject);
            }
            for (int i = 0; i < equipmentManager.GetEquipsList.Length; i++)
            {
                EquipmentUISlot emptyItem = Instantiate(equipmentContainer);
                emptyItem.transform.SetParent(EquipmentPanel);
                emptyItem.SlotType = SlotType.EQUIPMENT;
                emptyItem.EquipType = (Enums.EquipTypes)i;
                equipmentSlotList.Add(emptyItem);

                emptyItem.SetItem(equipmentManager.GetEquipsList[i]);
            }
        }
    }
}
