using Advent.Entities;
using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Manager
{
    public class StatsManager : MonoBehaviour
    {
        public static StatsManager instance;
        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }
        public void InitStats(Stats stats, EntityStats entityStats)
        {
            stats.strength.AddStat(entityStats.strength);
            stats.agility.AddStat(entityStats.agility);
            stats.vitality.AddStat(entityStats.vitality);
            stats.intelligence.AddStat(entityStats.intelligence);

            stats.attack = new StatRange();
            stats.defense = new StatRange();
        }

        public int InitMaxHP(int vitality, int level, int multiplier)
        {
            return (vitality + level) * multiplier;
        }
        public int InitMaxMP(int intelligence, int level, int multiplier)
        {
            return (intelligence + level) * multiplier;
        }
    }
}