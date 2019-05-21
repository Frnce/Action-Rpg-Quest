using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Utilities;
using Advent.Interfaces;
using Advent.Manager;
using TMPro;

namespace Advent.Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField]
        protected EntityStats entityStats = null;
        [SerializeField]
        protected Stats statList = null;
        [SerializeField]
        protected float hpMultiplier = 0;
        [SerializeField]
        protected float mpMultiplier = 0;
        [Space]
        [SerializeField]
        protected GameObject floatingDamageText;
        [SerializeField]
        protected float knockbackDistance;
        [SerializeField]
        protected GameObject hurtBox = null;
        [SerializeField]
        protected SpriteRenderer sprite;

        protected int currentLevel = 0;
        protected float currentHP = 0;
        protected float currentMP = 0;

        protected Rigidbody2D rb2d;
        protected Animator anim;

        private StatsManager statManager;
        private StatFormulas statFormula = new StatFormulas();

        public virtual void Start()
        {
            statManager = StatsManager.instance;
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            if (entityStats.enemyLevel <= 0)
            {
                currentLevel = entityStats.enemyLevel;
            }
            else
            {
                currentLevel = PlayerLevelManager.instance.GetCurrentLevel;
            }

            InitStats();
        }

        public void InitStats()
        {
            statManager.InitStats(statList);

            statList.strength.baseValue = entityStats.strength;
            statList.dexterity.baseValue = entityStats.dexterity;
            statList.intelligence.baseValue = entityStats.intelligence;
            statList.vitality.baseValue = entityStats.vitality;

            statList.maxHitPoints.baseValue = statFormula.ComputeMaxHP(statList.vitality.getValue, currentLevel, 3);
            statList.maxManaPoints.baseValue = statFormula.ComputeMaxMP(statList.intelligence.getValue, currentLevel, 2);

            statList.movementSpeed.baseValue = entityStats.movementSpeed;

            IntRange baseAttackResult = statFormula.ComputeBaseAttack(statList.strength.getValue, currentLevel, statList.weaponDamage.minDamage.getValue, statList.weaponDamage.maxDamage.getValue);
            statList.baseAttack = baseAttackResult;

            currentHP = statList.maxHitPoints.getValue;
            currentMP = statList.maxManaPoints.getValue;
        }
        public float GetCurrentHP
        {
            get
            {
                return Mathf.Round(currentHP);
            }
        }
        public float GetCurrentMP
        {
            get
            {
                return Mathf.Round(currentMP);
            }
        }
        public float GetMaxHP
        {
            get
            {
                return Mathf.Round(statList.maxHitPoints.getValue);
            }
        }
        public float GetMaxMP
        {
            get
            {
                return Mathf.Round(statList.maxManaPoints.getValue);
            }
        }
        public virtual void Die()
        {
            Debug.Log(gameObject.name + " Died");
        }
        protected void ShowFloatingDamageText(float damageAmount)
        {
            GameObject obj = Instantiate(floatingDamageText, transform.position, Quaternion.identity, transform);
            obj.GetComponentInChildren<TMP_Text>().text = "- " + damageAmount;
        }
    }
}