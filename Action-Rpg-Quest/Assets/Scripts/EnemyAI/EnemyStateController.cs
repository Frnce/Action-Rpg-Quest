using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.AI
{
    public class EnemyStateController : MonoBehaviour
    {
        public EnemyState currentState;
        private bool isAiActive = true;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!isAiActive)
            {
                return;
            }
            currentState.UpdateState(this);
        }
    }
}