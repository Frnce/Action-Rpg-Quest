using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.AI
{
    [CreateAssetMenu(menuName ="Enemies/AI/State")]
    public class EnemyState : ScriptableObject
    {
        public EnemyAction[] action;

        public void UpdateState(EnemyStateController controller)
        {
            DoActions(controller);
        }

        private void DoActions(EnemyStateController controller)
        {
            for (int i = 0; i < action.Length; i++)
            {
                action[i].Act(controller);
            }
        }
    } 
}