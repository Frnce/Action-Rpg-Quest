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

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void PerformAttack(int damage)
        {
            CurrentDamage = damage;
            animator.SetTrigger("AttackSword");
        }
    }

}