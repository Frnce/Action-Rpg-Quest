using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(menuName = "Items/Equipment/Foot", fileName = "Foot")]
    public class Foot : Equipment
    {
        public Foot()
        {
            slots = EquipSlots.FOOT;
        }
    }
}