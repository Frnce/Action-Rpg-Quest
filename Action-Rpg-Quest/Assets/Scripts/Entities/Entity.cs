using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Utilities;

namespace Advent.Entities
{
    [System.Serializable]
    public class Stats
    {
        public Stat strength;
        public Stat agility;
        public Stat vitality;
        public Stat intelligence;
    }
    public class Entity : MonoBehaviour
    {
        [SerializeField]
        protected EntityStats entityStats = null;
        [SerializeField]
        protected float movementSpeed = 10f;
        protected Rigidbody2D rb2d;
        protected Animator anim;

        [SerializeField]
        protected Stats statList;

        private void InitStats()
        {
            statList.strength.AddStat(entityStats.strength);
            statList.agility.AddStat(entityStats.agility);
            statList.vitality.AddStat(entityStats.vitality);
            statList.intelligence.AddStat(entityStats.intelligence);
        }
        public virtual void Start()
        {
            InitStats();
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }
        public virtual void Die()
        {
            Debug.Log(gameObject.name + " Died");
        }
    }
}