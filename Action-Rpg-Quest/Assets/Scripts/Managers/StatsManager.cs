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
            stats.dexterity.AddStat(entityStats.dexterity);
            stats.vitality.AddStat(entityStats.vitality);
            stats.intelligence.AddStat(entityStats.intelligence);
        }

        public float InitMaxHP(float vitality, float level, float multiplier)
        {
            return (vitality + level) * multiplier;
        }
        public float InitMaxMP(float intelligence, float level, float multiplier)
        {
            return (intelligence + level) * multiplier;
        }
    }
}