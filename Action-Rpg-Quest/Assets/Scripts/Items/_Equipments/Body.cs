using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(menuName = "Items/Equipment/Body", fileName = "New Body")]
    public class Body : Equipment
    {
        public Body()
        {
            slots = EquipSlots.BODY;
        }
    }
}