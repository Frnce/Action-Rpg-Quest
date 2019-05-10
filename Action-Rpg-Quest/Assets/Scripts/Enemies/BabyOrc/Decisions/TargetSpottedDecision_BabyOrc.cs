using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.AI.Enemy
{
    [CreateAssetMenu(menuName = "Enemies/Enemy/BabyOrc/Decision/TargetSpottedDecision")]
    public class TargetSpottedDecision_BabyOrc : Decision
    {
        public LayerMask playerLayer;
        public float lookRadius;
        public override bool Decide(StateController controller)
        {
            bool hasTarget = HasTarget(controller);
            return hasTarget;
        }
        private bool HasTarget(StateController controller)
        {
            if (Physics2D.OverlapCircle(controller.transform.position, lookRadius, playerLayer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}