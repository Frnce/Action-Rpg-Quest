using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Interfaces;
using Advent.Utilities;

namespace Advent.Entities
{
    public class MeleeHitEnemy : MonoBehaviour
    {
        private Stats stats;
        private void Start()
        {
            stats = Player.instance.GetStats;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //TODO Make layermask modular . using a variable
            if (collision.CompareTag("Enemy") && collision.gameObject.layer == LayerMask.NameToLayer("Hurtbox"))
            {
                collision.GetComponentInParent<IDamageable>().TakeDamage(stats.strength.GetValue(),Vector3.zero);
            }
        }
    }
}