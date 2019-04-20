﻿using Advent.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.AI
{
    public class StateController : MonoBehaviour
    {
        public bool isAiActive = true;
        [SerializeField]
        private State currentState = null;
        [SerializeField]
        private State remainState = null;
        private EnemyController enemyController;

        [HideInInspector] public float stateTimeElapsed;

        // Start is called before the first frame update
        void Start()
        {
            enemyController = GetComponent<EnemyController>();
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
        public void TransitionToState(State nextState)
        {
            if (nextState != remainState)
            {
                currentState = nextState;
                OnExitState();
            }
        }
        public bool CheckIfCountdownElapsed(float duration)
        {
            stateTimeElapsed += Time.deltaTime;
            return stateTimeElapsed >= duration;
        }
        private void OnExitState()
        {
            stateTimeElapsed = 0;
        }
        public EnemyController Enemy()
        {
            return enemyController;
        }
    }
}