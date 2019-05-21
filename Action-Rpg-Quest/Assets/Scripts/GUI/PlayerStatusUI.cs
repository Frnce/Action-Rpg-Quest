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

        private float hpPercent = 100;
        private float mpPercent = 100;
        private float expPercent = 0;

        private Player player;
        private PlayerLevelManager playerLevel;
        // Start is called before the first frame update
        void Start()
        {
            player = Player.instance;
            playerLevel = PlayerLevelManager.instance;
        }

        // Update is called once per frame
        void Update()
        {
            HpUpdateUI();
            MpUpdateUI();
            ExpUpdateUI();
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
            mpPercent = (100f / player.GetMaxMP) * player.GetCurrentMP;
            mpBar.fillAmount = mpPercent / 100f;
            currentMP.text = player.GetCurrentMP.ToString();
            maxMP.text = player.GetMaxMP.ToString();
        }
        private void ExpUpdateUI()
        {
            levelText.text = playerLevel.GetCurrentLevel.ToString();

            expPercent = (100f / playerLevel.GetExpNeeded) * playerLevel.GetCurrentExp;
            expBar.fillAmount = expPercent / 100f;
        }
    }
}