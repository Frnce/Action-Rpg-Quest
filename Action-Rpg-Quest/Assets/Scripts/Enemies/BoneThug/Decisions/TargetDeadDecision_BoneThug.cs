using Advent.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.AI.Enemy
{
    [CreateAssetMenu(menuName = "Enemies/Enemy/BoneThug/Decision/TargetDeadDecision")]
    public class TargetDeadDecision_BoneThug : Decision
    {
        public override bool Decide(StateController controller)
        {
            EnemyController enemy = controller.Enemy();
            GameObject target = enemy.GetTarget();
            bool chaseTargetIsActive = target.gameObject.activeSelf;
            return chaseTargetIsActive;
        }
    }
}