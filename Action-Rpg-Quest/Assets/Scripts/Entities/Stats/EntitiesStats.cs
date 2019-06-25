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

                new BaseStat(BaseStat.BaseStatType.BONUS_ASPD,0,"Bonus_Aspd"),
                new BaseStat(BaseStat.BaseStatType.BONUS_MS,0,"Bonus_Ms"),

                new BaseStat(BaseStat.BaseStatType.P_DEF,0,"P_Def"),
                new BaseStat(BaseStat.BaseStatType.M_DEF,0,"M_Def"),

                new BaseStat(BaseStat.BaseStatType.CRIT_CHANCE,0,"Crit_Chance"),
                new BaseStat(BaseStat.BaseStatType.CRIT_DMG_PERCENT,0,"Crit_Dmg_Percent"),

                //Mods
                new BaseStat(BaseStat.BaseStatType.P_DMG_INCREASE,0,"P_Dmg_Inc"),
                new BaseStat(BaseStat.BaseStatType.M_DMG_INCREASE,0,"M_Dmg_Inc"),
                new BaseStat(BaseStat.BaseStatType.LIFESTEAL_PERCENT_CHANCE,0,"Lifesteal_Chance"),
                new BaseStat(BaseStat.BaseStatType.LIFESTEAL_PERCENT_AMOUNT,0,"Lifesteal_Amount"),
                new BaseStat(BaseStat.BaseStatType.HP_BONUS_PERCENT,0,"HP_Bonus"),
                new BaseStat(BaseStat.BaseStatType.MP_BONUS_PERCENT,0,"MP_Bonus"),
                new BaseStat(BaseStat.BaseStatType.HP_REGEN_PERCENT,0,"HP_Regen"),
                new BaseStat(BaseStat.BaseStatType.MP_REGEN_PERCENT,0,"MP_Regen"),

                new BaseStat(BaseStat.BaseStatType.BLOCK_CHANCE_SECOND,0,"Block_Chance"),
                
                new BaseStat(BaseStat.BaseStatType.CAST_TIME_REDUC,0,"Cast_Time_Reduction"),
                new BaseStat(BaseStat.BaseStatType.COOLDOWN_REDUC,0,"Cooldown_Reduction"),
        };
        }

        public BaseStat GetStat(BaseStat.BaseStatType stat)
        {
            return stats.Find(x => x.StatType == stat);
        }

        public void AddStatBonus(Dictionary<string, BaseStat> statBonuses)
        {
            foreach (KeyValuePair<string,BaseStat> entry in statBonuses)
            {
                GetStat(entry.Value.StatType).AddStatBonus(new StatBonus(entry.Value.BaseValue));
            }
        }

        public void RemoveStatBonus(Dictionary<string, BaseStat> statBonuses)
        {
            foreach (KeyValuePair<string, BaseStat> entry in statBonuses)
            {
                GetStat(entry.Value.StatType).RemoveStatBonus(new StatBonus(entry.Value.BaseValue));
            }
        }
    }
}