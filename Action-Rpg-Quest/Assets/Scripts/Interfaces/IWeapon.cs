using Advent.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Interfaces
{
    public interface IWeapon
    {
        List<BaseStat> Stats { get; set; }
        int CurrentDamage { get; set; }
        void PerformAttack(int damage);
    }
}
