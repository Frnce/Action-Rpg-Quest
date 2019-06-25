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

        //Stats
        protected int currentLevel = 0;
        protected float currentHP = 0;
        protected float currentMP = 0;
        protected float maxHP = 0;
        protected float maxMP = 0;
        protected IntRange baseAttack = new IntRange(0, 0);

        protected float bonusStr = 0;
        protected float bonusDex = 0;
        protected float bonusAgi = 0;
        protected float bonusVit = 0;
        protected float bonusInt = 0;

        protected float currentPDef = 0;
        protected float currentMDef = 0;

        protected float critChance = 0;
        protected float critMultiplier = 0; //Damage

        protected float movementSpeedBonus = 0;
        protected float attackSpeed = 0;

        protected float pDmgIncrease = 0;
        protected float mDmgIncrease = 0;
        protected float lifeStealAmount = 0;
        protected float lifeStealChance = 0;
        protected float hpBonusPercent = 0;
        protected float hpRegenPercent = 0;
        protected float mpBonusPercent = 0;
        protected float mpRegenPercent = 0;
        protected float blockChanceSecond = 0;
        protected float castTimeReduction = 0;
        protected float cooldownReduction = 0;

        protected Rigidbody2D rb2d;
        protected Animator anim;

        private StatsManager statManager;
        protected virtual void Start()
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
        protected virtual void InitStats()
        {
            Debug.Log("ReInitializing Stats");

            bonusStr = entitiesStats.GetStat(BaseStat.BaseStatType.BONUS_STR).GetCalculatedStatValue();
            bonusDex = entitiesStats.GetStat(BaseStat.BaseStatType.BONUS_DEX).GetCalculatedStatValue();
            //bonusAgi = entitiesStats.GetStat(BaseStat.BaseStatType.bonus)
            bonusVit = entitiesStats.GetStat(BaseStat.BaseStatType.BONUS_VIT).GetCalculatedStatValue();
            bonusInt = entitiesStats.GetStat(BaseStat.BaseStatType.BONUS_INT).GetCalculatedStatValue();

            critChance = entitiesStats.GetStat(BaseStat.BaseStatType.CRIT_CHANCE).GetCalculatedStatValue();
            critMultiplier = entitiesStats.GetStat(BaseStat.BaseStatType.CRIT_DMG_PERCENT).GetCalculatedStatValue();

            movementSpeedBonus = entitiesStats.GetStat(BaseStat.BaseStatType.BONUS_MS).GetCalculatedStatValue();
            attackSpeed = entitiesStats.GetStat(BaseStat.BaseStatType.BONUS_ASPD).GetCalculatedStatValue();

            pDmgIncrease = entitiesStats.GetStat(BaseStat.BaseStatType.P_DMG_INCREASE).GetCalculatedStatValue();
            mDmgIncrease = entitiesStats.GetStat(BaseStat.BaseStatType.M_DMG_INCREASE).GetCalculatedStatValue();

            lifeStealChance = entitiesStats.GetStat(BaseStat.BaseStatType.LIFESTEAL_PERCENT_CHANCE).GetCalculatedStatValue();
            lifeStealAmount = entitiesStats.GetStat(BaseStat.BaseStatType.LIFESTEAL_PERCENT_AMOUNT).GetCalculatedStatValue();

            hpBonusPercent = entitiesStats.GetStat(BaseStat.BaseStatType.HP_BONUS_PERCENT).GetCalculatedStatValue();
            mpBonusPercent = entitiesStats.GetStat(BaseStat.BaseStatType.MP_BONUS_PERCENT).GetCalculatedStatValue();

            hpRegenPercent = entitiesStats.GetStat(BaseStat.BaseStatType.HP_REGEN_PERCENT).GetCalculatedStatValue();
            mpRegenPercent = entitiesStats.GetStat(BaseStat.BaseStatType.MP_REGEN_PERCENT).GetCalculatedStatValue();

            blockChanceSecond = entitiesStats.GetStat(BaseStat.BaseStatType.BLOCK_CHANCE_SECOND).GetCalculatedStatValue();

            castTimeReduction = entitiesStats.GetStat(BaseStat.BaseStatType.CAST_TIME_REDUC).GetCalculatedStatValue();
            cooldownReduction = entitiesStats.GetStat(BaseStat.BaseStatType.COOLDOWN_REDUC).GetCalculatedStatValue();
        }
        protected void InitHitpoints(EntitiesStats entitiesStats,int level)
        {
            maxHP = statManager.InitMaxHP(entitiesStats, level);
            currentHP = maxHP;
        }
        protected void InitManaPoints(EntitiesStats entitiesStats,int level)
        {
            maxMP = statManager.InitMaxMP(entitiesStats,level);
            currentMP = maxMP;
        }
        protected void InitBaseDamage(EntitiesStats entitiesStats,int level)
        {
            baseAttack = statManager.InitDamage(entitiesStats, level);
            Debug.Log("Min Damage : " + baseAttack.m_Min + " Max Damage : " + baseAttack.m_Max);
        }
        protected void InitPhysicalDefense(EntitiesStats entitiesStats)
        {
            currentPDef = statManager.InitPDef(entitiesStats);
            Debug.Log(currentPDef);
        }
        protected void InitMagicDefense(EntitiesStats entitiesStats)
        {
            currentMDef = statManager.InitMDef(entitiesStats);
        }
        public int GetCalculatedDamage(IntRange _baseAttack,EntitiesStats entitiesStats,int targetDef)
        {
            int value = statManager.GetCalculatedDamage(_baseAttack, entitiesStats, targetDef);
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