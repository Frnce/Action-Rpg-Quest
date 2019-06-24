using Advent.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Interfaces
{
    public interface IWeapon
    {
        Dictionary<string, BaseStat> Stats { get; set; }
        int CurrentDamage { get; set; }
        AudioClip[] AudioClip { get; set; }
        void PerformAttack(float attackSpeed);
        void ResetAttackTrigger();
    }
}
