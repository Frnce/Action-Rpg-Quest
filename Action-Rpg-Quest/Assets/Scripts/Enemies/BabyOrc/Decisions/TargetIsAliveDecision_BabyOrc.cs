using Advent.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.AI.Enemy
{
    [CreateAssetMenu(menuName = "Enemies/Enemy/BabyOrc/Decision/TargetDeadDecision")]
    public class TargetIsAliveDecision_BabyOrc :Decision
    {
        //DECISION should always return bool
        public override bool Decide(StateController controller)
        {
            EnemyController enemy = controller.Enemy();
            GameObject target = enemy.GetTarget();
            bool chaseTargetIsActive = target.gameObject.activeSelf;
            return chaseTargetIsActive;
        }
    }
}