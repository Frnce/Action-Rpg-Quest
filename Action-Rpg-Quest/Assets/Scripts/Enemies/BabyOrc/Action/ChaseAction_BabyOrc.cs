using Advent.AI;
using Advent.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.AI.Enemy
{
    [CreateAssetMenu(menuName = "Enemies/Enemy/BabyOrc/Actions/ChaseAction")]
    public class ChaseAction_BabyOrc : Action
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
            Vector2 direction = Vector2.MoveTowards(controller.transform.position, target.transform.position, enemy.GetMovementSpeed() * Time.deltaTime);
            enemy.Movement(direction);
        }
    }
}