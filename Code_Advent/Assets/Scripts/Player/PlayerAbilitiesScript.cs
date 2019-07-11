using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Player
{
    public class PlayerAbilitiesScript : MonoBehaviour
    {
        private Vector2 mouseControls;
        private PlayerControlsScript playerControls;

        //For Player Attack Setting
        private float timeBetweenAttack;
        [Tooltip("startTimeBetweenAttack")]
        public float startTimeBetweenAttack;
        private void Start()
        {
            playerControls = PlayerControlsScript.instance;

            timeBetweenAttack = startTimeBetweenAttack;
        }
        private void Update()
        {
            PlayerAttack();
        }
        private void PlayerAttack()
        {
            if (timeBetweenAttack <= 0)
            {
                if (playerControls.GetAttackKey)
                {
                    StartCoroutine(AttackRoutine());
                    timeBetweenAttack = startTimeBetweenAttack;
                }
            }
            else
            {
                timeBetweenAttack -= Time.deltaTime;
            }
        }
        private IEnumerator AttackRoutine()
        {
            playerControls.canPlayerMove = false;
            playerControls.GetAnim.SetTrigger("Attack");

            yield return new WaitForSeconds(startTimeBetweenAttack);

            playerControls.canPlayerMove = true;
        }
    }
}