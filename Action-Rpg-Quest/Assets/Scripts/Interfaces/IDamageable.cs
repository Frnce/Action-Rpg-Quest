using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Interfaces
{
    public interface IDamageable
    {
        //targetPoint = Where is the damage came from
        //If targetpoint is not in use , put vector3.zero instead
        void TakeDamage(int damage,Vector3 targetPoint);
    }
}