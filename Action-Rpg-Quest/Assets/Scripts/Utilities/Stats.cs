using Advent.Entities;

namespace Advent.Utilities
{
    [System.Serializable]
    public class AttackDamageRange
    {
        public EntityStat minDamage;
        public EntityStat maxDamage;
    }
    [System.Serializable]
    public class Stats
    {
        //Base Attribute
        public EntityStat strength;
        public EntityStat dexterity;
        public EntityStat vitality;
        public EntityStat intelligence;

        public EntityStat maxHitPoints;
        public EntityStat maxManaPoints;

        public AttackDamageRange weaponDamage;
        public IntRange baseAttack;
        public AttackDamageRange magicalAttack;

        public EntityStat physicalDefense;
        public EntityStat magicalDefense;

        public EntityStat movementSpeed;

        //Modifiers
        public EntityStat criticalHitChance;
        public EntityStat criticalHitDamage;

        public EntityStat ignorePhysicalDefense;
        public EntityStat ignoreMagicalDefense;
        //public Stat ignorePhysicalDefenseChance;
        //public Stat ignoreMagicalDefenseChance;

        public EntityStat healthRegen; // x per second
        public EntityStat manaRegen; // x per second

        //Modifier
        public EntityStat lifeStealPercent;
        public EntityStat abilityCooldownReduction;
    }
}