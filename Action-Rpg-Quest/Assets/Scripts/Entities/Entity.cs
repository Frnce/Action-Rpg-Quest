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
        [Space]
        [SerializeField]
        protected GameObject floatingDamageText;
        [SerializeField]
        protected float knockbackDistance;
        [SerializeField]
        protected GameObject hurtBox = null;
        [SerializeField]
        protected SpriteRenderer sprite;

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
        public float GetCurrentHP()
        {
            return Mathf.Round(currentHP);
        }
        public float GetCurrentMP()
        {
            return Mathf.Round(currentMP);
        }
        public float GetMaxHP()
        {
            return Mathf.Round(maxHP);
        }
        public float GetMaxMP()
        {
            return Mathf.Round(maxMP);
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