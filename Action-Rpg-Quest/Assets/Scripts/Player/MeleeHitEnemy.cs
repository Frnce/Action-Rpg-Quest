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
                int damage = collision.GetComponentInParent<EnemyController>().GetDamage(stats.baseAttack, stats.weaponDamage,stats.PdmgIncreaseMod);
                collision.GetComponentInParent<IDamageable>().TakeDamage(damage,Vector3.zero);//TODO GET DAMAGE FROM THE ATTACK ATTRIBUTES
            }
        }
    }
}