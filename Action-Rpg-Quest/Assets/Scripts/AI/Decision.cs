using Advent.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.AI
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(StateController controller);
    }
}
