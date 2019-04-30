using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(menuName = "Items/Quest", fileName = "New Quest Item")]
    public class QuestItem : Item
    {
        public override void Use()
        {
            base.Use();
        }
    }
}