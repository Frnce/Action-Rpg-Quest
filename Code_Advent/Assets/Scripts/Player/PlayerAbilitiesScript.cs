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

        public GameObject playerHand;

        private Vector2 mouse;
        private void Start()
        {
            playerControls = PlayerControlsScript.instance;

            timeBetweenAttack = startTimeBetweenAttack;
        }
        private void Update()
        {
            Aim();
            PlayerAttack();
        }
        private void Aim()
        {
            //The Weapon should be facing up or vector2.up for it to work properly
            mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);        //Mouse position
            Vector3 objpos = Camera.main.WorldToViewportPoint(playerHand.transform.position);        //Object position on screen
            Vector2 relobjpos = new Vector2(objpos.x - 0.5f, objpos.y - 0.5f);            //Set coordinates relative to object
            Vector2 relmousepos = new Vector2(mouse.x - 0.5f, mouse.y - 0.5f) - relobjpos;
            float angle = Vector2.Angle(Vector2.up, relmousepos);    //Angle calculation
            if (relmousepos.x > 0)
                angle = 360 - angle;
            Quaternion quat = Quaternion.identity;
            quat.eulerAngles = new Vector3(0, 0, angle); //Changing angle
            playerHand.transform.rotation = quat;
        }
        private void PlayerAttack()
        {
            if (timeBetweenAttack <= 0)
            {
                if (playerControls.GetAttackKey)
                {
                    playerControls.GetAnim.SetTrigger("Attack");
                    //StartCoroutine(AttackRoutine());
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
            yield return new WaitForSeconds(startTimeBetweenAttack);
            playerControls.canPlayerMove = true;
        }
    }
}