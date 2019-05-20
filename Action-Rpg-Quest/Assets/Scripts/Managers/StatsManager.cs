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
            //Basic Attribute
            stats.strength = new EntityStat(entityStats.strength);
            stats.dexterity = new EntityStat(entityStats.dexterity);
            stats.vitality = new EntityStat(entityStats.vitality);
            stats.intelligence = new EntityStat(entityStats.intelligence);

            //Movement Speed
            stats.movementSpeed = new EntityStat(entityStats.movementSpeed);

            //HP MP
            stats.maxHitPoints = new EntityStat(InitMaxHP(stats.vitality.getValue,3));
            stats.maxManaPoints = new EntityStat(InitMaxMP(stats.intelligence.getValue,2));
        }

        public float InitMaxHP(float vitality, float multiplier)
        {
            return (vitality) * multiplier; //TODO Add entity level to the equation HP
        }
        public float InitMaxMP(float intelligence, float multiplier)
        {
            return (intelligence) * multiplier; //TODO Add entity level to the equation MP
        }
    }
}