using Advent.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    public class Consumable : Item
    {
        public Consumable()
        {
            itemType = ItemTypes.CONSUMABLE;
        }
    }
}