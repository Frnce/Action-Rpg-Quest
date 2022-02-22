using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public class EntitiesStats
    {
        public List<BaseStat> stats = new List<BaseStat>();

        public EntitiesStats(int baseStr,int baseDex, int baseVit, int baseInt)
        {
            stats = new List<BaseStat>()
            {
                new BaseStat(BaseStat.BaseStatType.BASE_STR,baseStr,"Base_Str"),
                new BaseStat(BaseStat.BaseStatType.BASE_DEX,baseDex,"Base_Dex"),
                new BaseStat(BaseStat.BaseStatType.BASE_VIT,baseVit,"Base_Vit"),
                new BaseStat(BaseStat.BaseStatType.BASE_INT,baseInt,"Base_Int"),

                new BaseStat(BaseStat.BaseStatType.P_ATK_MIN,0,"P_Atk_Min"),
                new BaseStat(BaseStat.BaseStatType.P_ATK_MAX,0,"P_Atk_Max"),

                new BaseStat(BaseStat.BaseStatType.Phy_Defense,0,"Phy_Defense")
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