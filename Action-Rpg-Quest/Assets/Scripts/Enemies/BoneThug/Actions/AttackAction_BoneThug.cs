using Advent.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.AI.Enemy
{
    [CreateAssetMenu(menuName = "Enemies/Enemy/BoneThug/Actions/AttackAction")]
    public class AttackAction_BoneThug : Action
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
                enemy.SetMovementAnimation(target.transform.position - controller.transform.position);
                enemy.GetAnimator().SetTrigger("attack");
                controller.stateTimeElapsed = 0;
            }
        }
    }
}