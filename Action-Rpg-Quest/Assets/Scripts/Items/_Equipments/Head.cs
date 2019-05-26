using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(menuName = "Items/Equipment/Head", fileName = "New Head")]
    public class Head : Equipment
    {
        public Head()
        {
            slots = EquipSlots.HEAD;
        }
    }
}