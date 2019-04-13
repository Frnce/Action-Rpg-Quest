using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Utilities;

namespace Advent.Entities
{
    [CreateAssetMenu(fileName = "New Entity Stats",menuName = "Entities/Base Stats")]
    public class EntityStats : ScriptableObject
    {
        new public string name;
        public int strength;
        public int agility;
        public int vitality;
        public int intelligence;
    }
}