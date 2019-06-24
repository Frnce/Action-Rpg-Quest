using Advent.Entities;
using Advent.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    public abstract class DaggerScript : MonoBehaviour, IWeapon
    {
        private Animator animator;
        [Header("Audio")]
        public AudioClip[] audioClip;

        public Dictionary<string, BaseStat>  Stats { get; set; }
        public int CurrentDamage { get; set; }
        public AudioClip[] AudioClip { get; set; }

        protected virtual void Start()
        {
            animator = GetComponent<Animator>();
            AudioClip = audioClip;
        }
        protected virtual void Update()
        {

        }
        public virtual void PerformAttack(float attackSpeed)
        {
            animator.SetBool("isAttacking", true);
            animator.SetFloat("multiplier", attackSpeed);
        }

        public virtual void ResetAttackTrigger()
        {
            Debug.Log("Reset Attack Animator");
        }
    }
}
