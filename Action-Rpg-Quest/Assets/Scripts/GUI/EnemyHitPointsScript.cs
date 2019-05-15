using Advent.Entities;
using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Advent.UI
{
    public class EnemyHitPointsScript : MonoBehaviour
    {
        private float hpPercent = 100;
        [SerializeField]
        private Image hitPointsBar = null;

        public EnemyController enemyBase;
        private void Update()
        {
            hpPercent = (100f / enemyBase.GetMaxHP()) * enemyBase.GetCurrentHP();

            hitPointsBar.fillAmount = hpPercent / 100f;
        }
    }
}