using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(menuName = "Items/Equipment/Arms", fileName = "New Arms")]
    public class Arms : Equipment
    {
        public Arms()
        {
            slots = EquipSlots.ARMS;
        }
    }
}