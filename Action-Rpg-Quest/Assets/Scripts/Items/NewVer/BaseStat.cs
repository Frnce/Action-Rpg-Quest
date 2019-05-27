using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public class BaseStat
    {
        public enum BaseStatType {STRENGTH,DEXTERITY,INTELLIGENCE,VITALITY}

        public List<StatBonus> baseAdditives { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public BaseStatType statType { get; set; }
        public int baseValue { get; set; }
        public string statName { get; set; }
        public string statDescription { get; set; }
        public int finalValue { get; set; }

        public BaseStat(int _baseValue, string _statName, string _statDescription)
        {
            baseAdditives = new List<StatBonus>();
            baseValue = _baseValue;
            statName = _statName;
            statDescription = _statDescription;
        }

        [JsonConstructor]
        public BaseStat(BaseStatType _statType, int _baseValue, string _statName)
        {
            baseAdditives = new List<StatBonus>();
            statType = _statType;
            baseValue = _baseValue;
            statName = _statName;
        }

        public void AddStatBonus(StatBonus statBonus)
        {
            baseAdditives.Add(statBonus);
        }

        public void RemoveStatBonus(StatBonus statBonus)
        {
            baseAdditives.Remove(baseAdditives.Find(x => x.bonusValue == statBonus.bonusValue));
        }

        public int GetCalculatedStatValue()
        {
            finalValue = 0;
            baseAdditives.ForEach(x => this.finalValue += x.bonusValue);
            this.finalValue += baseValue;
            return finalValue;
        }

    }
    public class StatBonus
    {
        public int bonusValue { get; set; }

        public StatBonus(int _bonusValue)
        {
            this.bonusValue = _bonusValue;
        }
    }
}