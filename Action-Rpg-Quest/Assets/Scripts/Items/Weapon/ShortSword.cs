using Advent.Entities;
using Advent.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    public class ShortSword : MonoBehaviour,IWeapon
    {
        private Animator animator;
        public List<BaseStat> Stats { get; set; }
        public int CurrentDamage { get; set; }
        [Header("Audio")]
        public AudioClip[] audioClip;
        public AudioClip[] AudioClip { get; set; }

        void Start()
        {
            animator = GetComponent<Animator>();
            AudioClip = audioClip;
        }

        public void PerformAttack(int damage)
        {
            CurrentDamage = damage;
            animator.SetTrigger("AttackSword");
        }

        public void ResetAttackTrigger()
        {
            animator.ResetTrigger("AttackSword");
        }
    }

}