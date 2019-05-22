﻿using System.Collections;
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
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            statManager = StatsManager.instance;
            if (entityStats.enemyLevel <= 0)
            {
                currentLevel = entityStats.enemyLevel;
            }
            else
            {
                currentLevel = PlayerLevelManager.instance.GetCurrentLevel;
            }
        }

        protected void InitStats()
        {
            statManager.InitStats(statList);

            statList.baseSTR = entityStats.strength;
            statList.baseDEX = entityStats.dexterity;
            statList.baseINT = entityStats.intelligence;
            statList.baseVIT = entityStats.vitality;

            statList.movementSpeed.baseValue = entityStats.movementSpeed;

            statManager.InitMaxHP(statList, statFormula,currentLevel); //HP
            statManager.InitMaxMP(statList, statFormula, currentLevel); // MP

            currentHP = statList.maxHitPoints.getValue;
            currentMP = statList.maxManaPoints.getValue;

            EquipmentManager.instance.EquipDefaults();

            statList.baseAttack = statFormula.ComputeBaseAttack(statList.baseSTR, statList.bonusSTR.getValue,currentLevel);
            statList.baseDef = statFormula.ComputeMaxDefense(statList.baseSTR, statList.armorDefense.getValue);
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
        public int GetCurrentLevel
        {
            get
            {
                return currentLevel;
            }
        }
        public Stats GetStats
        {
            get
            {
                return statList;
            }
        }
        public virtual void Die()
        {
            Debug.Log(gameObject.name + " Died");
        }
        public int GetDamage(IntRange baseAttack, AttackDamageRange weaponDamage)
        {
            return statFormula.ComputeDamage(baseAttack, weaponDamage, statList.baseDef, currentLevel);
        }
        protected void ShowFloatingDamageText(float damageAmount)
        {
            GameObject obj = Instantiate(floatingDamageText, transform.position, Quaternion.identity, transform);
            obj.GetComponentInChildren<TMP_Text>().text = "- " + damageAmount;
        }
    }
}