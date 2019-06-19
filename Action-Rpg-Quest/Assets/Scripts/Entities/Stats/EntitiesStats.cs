using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public class EntitiesStats
    {
        public List<BaseStat> stats = new List<BaseStat>();

        public int base_Str;
        public int base_Dex;
        public int base_Vit;
        public int base_Int;

        public EntitiesStats()
        {
            stats = new List<BaseStat>()
            {
                new BaseStat(BaseStat.BaseStatType.BONUS_STR,0,"Bonus_Str"),
                new BaseStat(BaseStat.BaseStatType.BONUS_DEX,0,"Bonus_Dex"),
                new BaseStat(BaseStat.BaseStatType.BONUS_VIT,0,"Bonus_Vit"),
                new BaseStat(BaseStat.BaseStatType.BONUS_INT,0,"Bonus_Int"),
                //Weapon Damage
                new BaseStat(BaseStat.BaseStatType.P_ATK_MIN,0,"P_Atk_Min"),
                new BaseStat(BaseStat.BaseStatType.P_ATK_MAX,0,"P_Atk_Max"),

                new BaseStat(BaseStat.BaseStatType.Phy_Defense,0,"P_Def"),

                new BaseStat(BaseStat.BaseStatType.CRIT_CHANCE,10,"Crit_Chance"),
                new BaseStat(BaseStat.BaseStatType.CRIT_DMG_PERCENT,35,"Crit_Dmg_Percent"),

                //Mods
                new BaseStat(BaseStat.BaseStatType.P_DMG_INCREASE,0,"P_Dmg_Inc")
        };
        }

        public BaseStat GetStat(BaseStat.BaseStatType stat)
        {
            return stats.Find(x => x.StatType == stat);
        }

        public void AddStatBonus(List<BaseStat> statBonuses)
        {
            foreach (BaseStat statBonus in statBonuses)
            {
                GetStat(statBonus.StatType).AddStatBonus(new StatBonus(statBonus.BaseValue));
            }
        }

        public void RemoveStatBonus(List<BaseStat> statBonuses)
        {
            foreach (BaseStat statBonus in statBonuses)
            {
                GetStat(statBonus.StatType).RemoveStatBonus(new StatBonus(statBonus.BaseValue));
            }
        }
    }
}