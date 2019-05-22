using Advent.Entities;
using Advent.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.AI.Enemy
{
    [CreateAssetMenu(menuName = "Enemies/Enemy/BabyOrc/Actions/AttackAction")]
    public class AttackAction_BabyOrc : Action
    {
        public float attackTime;
        public override void Act(StateController controller)
        {
            Attack(controller);
        }
        private void Attack(StateController controller)
        {
            EnemyController enemy = controller.Enemy();
            GameObject target = enemy.GetTarget();

            if (Vector2.Distance(controller.transform.position, target.transform.position) <= 1.5f && controller.CheckIfCountdownElapsed(attackTime))
            {
                //Vector3 direction = target.transform.position - controller.transform.position;
                //enemy.GetAnimator().SetFloat("xMove", direction.x);
                //enemy.GetAnimator().SetFloat("yMove", direction.y);
                //enemy.GetAnimator().SetTrigger("attack");

                //TODO Change this magic number to the stat of Enemy
                int damage = target.GetComponent<Player>().GetDamage(enemy.GetStats.baseAttack, enemy.GetStats.weaponDamage);
                target.GetComponent<IDamageable>().TakeDamage(damage,controller.transform.position);
                controller.stateTimeElapsed = 0;
            }
        }
    }

}