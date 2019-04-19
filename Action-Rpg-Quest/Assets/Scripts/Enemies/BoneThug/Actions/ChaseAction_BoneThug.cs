using Advent.AI;
using Advent.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.AI.Enemy
{
    [CreateAssetMenu(menuName = "Enemies/Enemy/BoneThug/Actions/ChaseAction")]
    public class ChaseAction_BoneThug : Action
    {
        public override void Act(StateController controller)
        {
            Chase(controller);
        }
        private void Chase(StateController controller)
        {
            EnemyController enemy = controller.Enemy();
            GameObject target = enemy.GetTarget();

            Vector2 newDirection = target.transform.position - controller.transform.position;
            enemy.GetAnimator().SetBool("isMoving", true);
            enemy.GetAnimator().SetFloat("xMove", newDirection.x);
            enemy.GetAnimator().SetFloat("yMove", newDirection.y);
            Vector2 direction = Vector2.MoveTowards(controller.transform.position, target.transform.position, enemy.GetMovementSpeed() * Time.deltaTime);
            enemy.Movement(direction);
        }
    }
}