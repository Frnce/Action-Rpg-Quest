using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.AI;
using Advent.Entities;

namespace Advent.AI.Enemy
{
    [CreateAssetMenu(menuName = "Enemies/Enemy/BoneThug/Actions/PatrolAction")]
    public class PatrolAction_BoneThug : Action
    {
        public float maxIdleTime = 1f;
        private float idleTime;
        public override void Act(StateController controller)
        {
            Patrol(controller);
        }
        private void Patrol(StateController controller)
        {
            EnemyController enemy = controller.Enemy();

            if (controller.transform.position == enemy.GetRandomPosition())
            {
                if (idleTime <= 0)
                {
                    enemy.SetRandomPosition();
                    Vector2 newDirection = enemy.GetRandomPosition() - controller.transform.position;
                    enemy.GetAnimator().SetBool("isMoving", true);
                    enemy.GetAnimator().SetFloat("xMove", newDirection.x);
                    enemy.GetAnimator().SetFloat("yMove", newDirection.y);

                    idleTime = maxIdleTime;
                }
                else
                {
                    enemy.GetAnimator().SetBool("isMoving", false);
                    idleTime -= Time.deltaTime;
                }
            }
            else
            {
                Vector2 direction = Vector2.MoveTowards(controller.transform.position, enemy.GetRandomPosition(), enemy.GetMovementSpeed() * Time.deltaTime);
                enemy.Movement(direction);
            }
        }
    }
}
