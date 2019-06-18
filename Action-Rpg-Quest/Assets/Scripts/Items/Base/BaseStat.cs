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
            BASE_STR,
            BONUS_STR,

            BASE_DEX,
            BONUS_DEX,

            BASE_INT,
            BONUS_INT,

            BASE_VIT,
            BONUS_VIT,

            P_ATK_MIN,
            P_ATK_MAX,

            P_ATK_MOD_PERCENT
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