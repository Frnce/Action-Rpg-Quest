using Advent.Entities;

namespace Advent.Utilities
{
    [System.Serializable]
    public class Stats
    {
        //Base Attribute
        public Stat strength;
        public Stat dexterity;
        public Stat vitality;
        public Stat intelligence;

        public Stat maxHitPoints;
        public Stat maxManaPoints;

        public StatRange physicalAttack;    
        public StatRange magicalAttack;

        public Stat physicalDefense;
        public Stat magicalDefense;

        public Stat movementSpeed;

        public Stat criticalHitChance;
        public Stat criticalHitDamage;

        public Stat ignorePhysicalDefense;
        public Stat ignoreMagicalDefense;
        //public Stat ignorePhysicalDefenseChance;
        //public Stat ignoreMagicalDefenseChance;

        public Stat healthRegen; // x per second
        public Stat manaRegen; // x per second

        //Modifier
        public Stat lifeStealPercent;
        public Stat abilityCooldownReduction;
    }
}