using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Utilities;
using Advent.Interfaces;
using Advent.Manager;

namespace Advent.Entities
{
    [System.Serializable]
    public class Stats
    {
        public Stat strength;
        public Stat agility;
        public Stat vitality;
        public Stat intelligence;
        public StatRange attack;
        public StatRange defense;
    }
    public class Entity : MonoBehaviour
    {
        [SerializeField]
        protected EntityStats entityStats = null;
        [SerializeField]
        protected float movementSpeed = 10f;
        protected Rigidbody2D rb2d;
        protected Animator anim;
        [Space]
        [SerializeField]
        protected int currentLevel = 0;
        [SerializeField]
        protected Stats statList = null;
        [Space]
        protected float currentHP = 0;
        protected float maxHP = 0;
        protected float currentMP = 0;
        protected float maxMP = 0;

        [SerializeField]
        protected float hpMultiplier = 0;
        [SerializeField]
        protected float mpMultiplier = 0;

        private StatsManager statManager;

        public virtual void Start()
        {
            statManager = StatsManager.instance;
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            InitStats();
        }

        private void InitStats()
        {
            statManager.InitStats(statList, entityStats);

            maxHP = statManager.InitMaxHP(statList.vitality.GetValue(), currentLevel, hpMultiplier);
            maxMP = statManager.InitMaxMP(statList.intelligence.GetValue(), currentLevel, mpMultiplier);

            currentHP = maxHP;
            currentMP = maxMP;
        }
        public virtual void Die()
        {
            Debug.Log(gameObject.name + " Died");
        }
    }
}