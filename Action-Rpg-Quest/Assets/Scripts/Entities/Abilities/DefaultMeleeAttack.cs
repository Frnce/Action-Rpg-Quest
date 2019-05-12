using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/MeleeAttack")]
    public class DefaultMeleeAttack : Ability
    {
        private MeleeAttack meleeAttack;
        public override void Initialize(GameObject obj)
        {
            meleeAttack = obj.GetComponent<MeleeAttack>();
        }

        public override void TriggerAbility()
        {
            meleeAttack.Attack();
        }
    }
}