using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(menuName = "Items/Equipment/Legs", fileName = "New Legs")]
    public class Legs : Equipment
    {
        public Legs()
        {
            slots = EquipSlots.LEGS;
        }
    }
}