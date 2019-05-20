using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Utilities;
using Advent.Items;

namespace Advent.Entities
{
    [CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Entities/Entity Base Stats")]
    public class EntityStats : ScriptableObject
    {
        new public string name;

        [Header("Attributes")]
        public int strength;
        public int dexterity;
        public int vitality;
        public int intelligence;
        [Space]
        public float movementSpeed;
        [Space]
        [Header("For Enemies")]
        public int enemyLevel;
        public int expGiven;
        public LootTable loot;
        [Range(0,100)]
        public int dropChance;
    }
}