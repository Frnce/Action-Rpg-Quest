using Advent.Entities;
using Advent.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    public class GreatSword : MonoBehaviour, IWeapon
    {
        private Animator animator;
        public Dictionary<string, BaseStat> Stats { get; set; }
        public int CurrentDamage { get; set; }
        [Header("Audio")]
        public AudioClip[] audioClip;
        public AudioClip[] AudioClip { get; set; }

        void Start()
        {
            animator = GetComponent<Animator>();
            AudioClip = audioClip;
        }

        public void PerformAttack(float attackSpeed)
        {
            animator.SetBool("isAttacking", true);
            animator.SetFloat("multiplier", attackSpeed);
        }

        public void ResetAttackTrigger()
        {
            Debug.Log("reset attack");
        }
    }
}