using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Advent.Entities;

namespace Advent.UI
{
    public class PlayerStatsUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text baseStr = null;
        [SerializeField]
        private TMP_Text bonusStr = null;

        [SerializeField]
        private TMP_Text baseDex = null;
        [SerializeField]
        private TMP_Text bonusDex = null;

        [SerializeField]
        private TMP_Text baseInt = null;
        [SerializeField]
        private TMP_Text bonueInt = null;

        [SerializeField]
        private TMP_Text baseVit = null;
        [SerializeField]
        private TMP_Text bonusVit = null;

        private Player player;

        private void Start()
        {
            player = Player.instance;
            UIEventHandlers.OnStatsChanged += InitStatsUI;
            InitStatsUI();
        }
        private void InitStatsUI()
        {
            InitBaseStats();
        }
        private void InitBaseStats()
        {
            baseStr.text = player.GetPlayerStats().base_Str.ToString();
            bonusStr.text = player.GetPlayerStats().GetStat(BaseStat.BaseStatType.BONUS_STR).GetCalculatedStatValue().ToString();

            baseDex.text = player.GetPlayerStats().base_Dex.ToString();
            bonusDex.text = player.GetPlayerStats().GetStat(BaseStat.BaseStatType.BONUS_DEX).GetCalculatedStatValue().ToString();

            baseInt.text = player.GetPlayerStats().base_Int.ToString();
            bonueInt.text = player.GetPlayerStats().GetStat(BaseStat.BaseStatType.BONUS_INT).GetCalculatedStatValue().ToString();

            baseVit.text = player.GetPlayerStats().base_Vit.ToString();
            bonusVit.text = player.GetPlayerStats().GetStat(BaseStat.BaseStatType.BONUS_VIT).GetCalculatedStatValue().ToString();
        }
    }
}