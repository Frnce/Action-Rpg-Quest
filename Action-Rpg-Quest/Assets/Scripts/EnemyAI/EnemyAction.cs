using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.AI
{
    public abstract class EnemyAction : ScriptableObject
    {
        public abstract void Act(EnemyStateController controller);
    }
}
