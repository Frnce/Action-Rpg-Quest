using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    [System.Serializable]
    public class BaseStat
    {
        public enum BaseStatType
        {
            BONUS_STR,
            BONUS_DEX,
            BONUS_INT,
            BONUS_VIT,

            P_ATK_MIN,
            P_ATK_MAX,

            BONUS_ASPD, // in percentage

            P_DEF,
            M_DEF,

            BONUS_MS, //in percentage

            CRIT_CHANCE,
            CRIT_DMG_PERCENT,

            P_DMG_INCREASE,
            M_DMG_INCREASE,
            LIFESTEAL_PERCENT_AMOUNT,
            LIFESTEAL_PERCENT_CHANCE,
            HP_BONUS_PERCENT,
            HP_REGEN_PERCENT,
            MP_BONUS_PERCENT,
            MP_REGEN_PERCENT,
            BLOCK_CHANCE_SECOND,
            CAST_TIME_REDUC,
            COOLDOWN_REDUC,
        }

        public List<StatBonus> BaseAdditives { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public BaseStatType StatType { get; set; }
        public int BaseValue { get; set; }
        public string StatName { get; set; }
        public string StatDescription { get; set; }
        public int FinalValue { get; set; }

        public BaseStat(int _baseValue, string _statName, string _statDescription)
        {
            BaseAdditives = new List<StatBonus>();
            BaseValue = _baseValue;
            StatName = _statName;
            StatDescription = _statDescription;
        }

        [JsonConstructor]
        public BaseStat(BaseStatType _statType, int _baseValue, string _statName)
        {
            BaseAdditives = new List<StatBonus>();
            StatType = _statType;
            BaseValue = _baseValue;
            StatName = _statName;
        }

        public void AddStatBonus(StatBonus statBonus)
        {
            BaseAdditives.Add(statBonus);
            Debug.Log("Bonus AddeD: " + statBonus.BonusValue);
        }

        public void RemoveStatBonus(StatBonus statBonus)
        {
            BaseAdditives.Remove(BaseAdditives.Find(x => x.BonusValue == statBonus.BonusValue));
        }

        public int GetCalculatedStatValue()
        {
            FinalValue = 0;
            BaseAdditives.ForEach(x => this.FinalValue += x.BonusValue);
            this.FinalValue += BaseValue;
            return FinalValue;
        }

    }
    public class StatBonus
    {
        public int BonusValue { get; set; }

        public StatBonus(int _bonusValue)
        {
            this.BonusValue = _bonusValue;
        }
    }
}