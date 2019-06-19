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
        protected EntitiesStats entitiesStats = null;
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
        protected float maxHP = 0;
        protected float maxMP = 0;
        protected IntRange baseAttack = new IntRange(0, 0);
        protected float currentPDef = 0;

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
        public void InitHitpoints(EntitiesStats entitiesStats,int level)
        {
            maxHP = statManager.InitMaxHP(entitiesStats, statFormula, level);
            currentHP = maxHP;
        }
        public void InitManaPoints(EntitiesStats entitiesStats,int level)
        {
            maxMP = statManager.InitMaxMP(entitiesStats,statFormula,level);
            currentMP = maxMP;
        }
        public void InitBaseDamage(EntitiesStats entitiesStats,int level)
        {
            baseAttack = statManager.InitDamage(entitiesStats, statFormula, level);
            Debug.Log("Min Damage : " + baseAttack.m_Min + " Max Damage : " + baseAttack.m_Max);
        }
        public void InitPhysicalDefense(EntitiesStats entitiesStats)
        {
            currentPDef = statManager.InitPDef(entitiesStats, statFormula);
            Debug.Log(currentPDef);
        }
        public int GetCalculatedDamage(IntRange _baseAttack,EntitiesStats entitiesStats,int targetDef)
        {
            int value = statManager.GetCalculatedDamage(_baseAttack, entitiesStats, targetDef, statFormula);
            return value;
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
                return Mathf.Round(maxHP);
            }
        }
        public float GetMaxMp
        {
            get
            {
                return Mathf.Round(maxMP);
            }
        }
        public int GetCurrentLevel
        {
            get
            {
                return currentLevel;
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