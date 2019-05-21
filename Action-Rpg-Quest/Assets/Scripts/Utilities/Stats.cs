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
        public float baseSTR;
        public float baseDEX;
        public float baseVIT;
        public float baseINT;

        public EntityStat bonusSTR;
        public EntityStat bonusDEX;
        public EntityStat bonusVIT;
        public EntityStat bonusINT;

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