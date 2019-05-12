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
        protected int currentHP = 0;
        protected int maxHP = 0;

        [SerializeField]
        protected int hpMultiplier = 0;

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

            currentHP = maxHP;
        }
        public virtual void Die()
        {
            Debug.Log(gameObject.name + " Died");
        }
    }
}