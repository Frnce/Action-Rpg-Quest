using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Advent.Entities;
using UnityEngine.UI;

namespace Advent.UI
{
    public class PlayerStatusUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text currentHP = null;
        [SerializeField]
        private TMP_Text maxHP = null;
        [SerializeField]
        private Image hpBar = null;
        private float hpPercent = 100;

        private Player player;
        // Start is called before the first frame update
        void Start()
        {
            player = Player.instance;
        }

        // Update is called once per frame
        void Update()
        {
            hpPercent = (100f / player.GetMaxHP()) * player.GetCurrentHP();

            hpBar.fillAmount = hpPercent / 100f;
            currentHP.text = player.GetCurrentHP().ToString();
            maxHP.text = player.GetMaxHP().ToString();
        }
    }
}