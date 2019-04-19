using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.AI
{
    [CreateAssetMenu(menuName ="AI/State")]
    public class State : ScriptableObject
    {
        [SerializeField]
        private Action[] action = null;
        [SerializeField]
        private Transition[] transitions = null;

        public void UpdateState(StateController controller)
        {
            DoActions(controller);
            CheckTransitions(controller);
        }

        private void DoActions(StateController controller)
        {
            for (int i = 0; i < action.Length; i++)
            {
                action[i].Act(controller);
            }
        }
        private void CheckTransitions(StateController controller)
        {
            for (int i = 0; i < transitions.Length; i++)
            {
                if (transitions[i].decision == null)
                {
                    Debug.LogError("Decision is empty");
                    return;
                }
                bool decisionSucceeded = transitions[i].decision.Decide(controller);
                if (decisionSucceeded)
                {
                    controller.TransitionToState(transitions[i].trueState);
                }
                else
                {
                    controller.TransitionToState(transitions[i].falseState);
                }
            }
        }
    } 
}