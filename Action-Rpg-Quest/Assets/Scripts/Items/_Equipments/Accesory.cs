using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(menuName = "Items/Equipment/Accesory", fileName = "New Accesory")]
    public class Accesory : Equipment
    {
        public Accesory()
        {
            slots = EquipSlots.ACCESORY;
        }
    }
}