using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Advent.Entities;
using UnityEngine.UI;
using Advent.Manager;

namespace Advent.UI
{
    public class PlayerStatusUI : MonoBehaviour
    {
        [Header("Player Stats Window")]
        [SerializeField]
        private GameObject statsWindow = null;
        [Space]
        [SerializeField]
        private TMP_Text currentHP = null;
        [SerializeField]
        private TMP_Text currentMP = null;
        [SerializeField]
        private TMP_Text maxHP = null;
        [SerializeField]
        private TMP_Text maxMP = null;

        [Space]
        [SerializeField]
        private Image hpBar = null;
        [SerializeField]
        private Image mpBar = null;
        [Space]
        [SerializeField]
        private TMP_Text levelText = null;
        [SerializeField]
        private Image expBar = null;

        [Header("Main Attributes - Base")]
        [SerializeField]
        private TMP_Text baseStrengthText = null;
        [SerializeField]
        private TMP_Text baseDexterityText = null;
        [SerializeField]
        private TMP_Text baseIntelligenceText = null;
        [SerializeField]
        private TMP_Text baseVitalityText = null;
        [Header("Main Attributes - Bonus")]
        [SerializeField]
        private TMP_Text bonusStrengthText = null;
        [SerializeField]
        private TMP_Text bonusDexterityText = null;
        [SerializeField]
        private TMP_Text bonusIntelligenceText = null;
        [SerializeField]
        private TMP_Text bonusVitalityText = null;
        [Header("Attack")]
        [SerializeField]
        private TMP_Text baseMinAttackText = null;
        [SerializeField]
        private TMP_Text baseMaxAttackText = null;
        [Header("Defense")]
        [SerializeField]
        private TMP_Text basePhysicalDefenseText = null;

        private float hpPercent = 100;
        private float mpPercent = 100;
        private float expPercent = 0;

        private Player player;
        private PlayerLevelManager playerLevel;

        private bool isStatsWindowActive = false;
        // Start is called before the first frame update
        void Start()
        {
            player = Player.instance;
            playerLevel = PlayerLevelManager.instance;

            statsWindow.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            //HP MP EXP
            HpUpdateUI();
            MpUpdateUI();
            ExpUpdateUI();
            if (Input.GetKeyDown(KeyCode.C))
            {
                isStatsWindowActive = !isStatsWindowActive;
            }

            statsWindow.SetActive(isStatsWindowActive);
            //ATTRIBUTES
            if (statsWindow.activeSelf)
            {
                GetMainAttributes();
                GetBaseAttack();
                GetBaseDefense();
            }
        }
        private void HpUpdateUI()
        {
            hpPercent = (100f / player.GetMaxHP) * player.GetCurrentHP;

            hpBar.fillAmount = hpPercent / 100f;
            currentHP.text = player.GetCurrentHP.ToString();
            maxHP.text = player.GetMaxHP.ToString();
        }
        private void MpUpdateUI()
        {
            mpPercent = (100f / player.GetMaxMp) * player.GetCurrentMP;
            mpBar.fillAmount = mpPercent / 100f;
            currentMP.text = player.GetCurrentMP.ToString();
            maxMP.text = player.GetMaxMp.ToString();
        }
        private void ExpUpdateUI()
        {
            levelText.text = playerLevel.GetCurrentLevel.ToString();

            expPercent = (100f / playerLevel.GetExpNeeded) * playerLevel.GetCurrentExp;
            expBar.fillAmount = expPercent / 100f;
        }
        private void GetMainAttributes()
        {
            //baseStrengthText.text = player.GetStats.baseSTR.ToString();
            //baseDexterityText.text = player.GetStats.baseDEX.ToString();
            //baseIntelligenceText.text = player.GetStats.baseINT.ToString();
            //baseVitalityText.text = player.GetStats.baseVIT.ToString();

            //bonusStrengthText.text = player.GetStats.bonusSTR.getValue.ToString();
            //bonusDexterityText.text = player.GetStats.bonusDEX.getValue.ToString();
            //bonusIntelligenceText.text = player.GetStats.bonusINT.getValue.ToString();
            //bonusVitalityText.text = player.GetStats.bonusVIT.getValue.ToString();
        }
        private void GetBaseAttack()
        {
            //baseMinAttackText.text = player.GetStats.baseAttack.m_Min.ToString();
            //baseMaxAttackText.text = player.GetStats.baseAttack.m_Max.ToString();
        }
        private void GetBaseDefense()
        {
            //basePhysicalDefenseText.text = player.GetStats.baseDef.ToString();
        }
    }
}