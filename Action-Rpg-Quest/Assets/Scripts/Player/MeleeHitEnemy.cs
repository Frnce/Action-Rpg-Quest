using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Interfaces;

namespace Advent.Entities
{
    public class MeleeHitEnemy : MonoBehaviour
    {
        private Stats stats;
        private void Start()
        {
            stats = Player.instance.GetStats();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<IDamageable>().TakeDamage(stats.strength.GetValue());
            }
        }
    }
}
