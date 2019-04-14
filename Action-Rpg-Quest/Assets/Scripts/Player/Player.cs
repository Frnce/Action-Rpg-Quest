using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Controller;
using Advent.Interfaces;
using Advent.Utilities;

namespace Advent.Entities
{
    public enum PlayerStates
    {
        IDLE,
        MOVING,
        ATTACKING,
        ROLLING
    }
    public class Player : Entity
    {
        public static Player instance;

        [SerializeField]
        private float rollSpeed = 5.0f;
        [SerializeField]
        private float rollDistance = 0.1f;

        private PlayerController playerControls = null;
        private Vector3 playerDir = Vector3.zero;
        private Vector3 playerLastDir = Vector3.zero;
        private PlayerStates states;
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.LogWarning("two or more instance of " + instance.gameObject.name + " detected.");
            }
            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            playerControls = PlayerController.instance;
            states = PlayerStates.IDLE;
        }
        // Update is called once per frame
        void Update()
        {
            playerDir.x = playerControls.GetXMovement();
            playerDir.y = playerControls.GetYMovement();
            if (states != PlayerStates.ROLLING)
            {
                if (playerControls.GetAttackKey())
                {
                    states = PlayerStates.ATTACKING;
                    StartCoroutine(AttackCoroutine());
                }
                if (playerControls.GetDodgeKey())
                {
                    if (playerDir != Vector3.zero)
                    {
                        states = PlayerStates.ROLLING;
                        StartCoroutine(DodgeRoll(rollSpeed, rollDistance));
                    }
                }
            }
        }
        private void FixedUpdate()
        {
            if(states != PlayerStates.ROLLING)
            {
                Movement();
            }
        }
        private IEnumerator DodgeRoll(float rollSpeed, float rollTime)
        {
            Physics2D.IgnoreLayerCollision(0, 8,true); //0 layer = DEFAULT / 8 layer = Enemies
            rb2d.velocity = new Vector2(playerDir.x * rollSpeed, playerDir.y * rollSpeed);
            yield return new WaitForSeconds(rollTime);
            Physics2D.IgnoreLayerCollision(0, 8, false);
            states = PlayerStates.IDLE;
        }
        private void Movement()
        {
            if (states != PlayerStates.ATTACKING)
            {
                SetDirectionAnimations();
                if (playerDir.x != 0 || playerDir.y != 0)
                {
                    states = PlayerStates.MOVING;
                }
                else
                {
                    states = PlayerStates.IDLE;
                }
                rb2d.velocity = new Vector2(Mathf.Lerp(0, playerDir.x * movementSpeed, 0.8f), //Change MovementSpeed to something from stats
                                                   Mathf.Lerp(0, playerDir.y * movementSpeed, 0.8f));
            }
            else
            {
                rb2d.velocity = Vector2.zero;
            }
        }
        private void SetAttackAnimations()
        {
            anim.SetTrigger("attack1");
        }
        private void SetDirectionAnimations()
        {
            if(playerDir != Vector3.zero)
            {
                if((playerDir.x > 0 || playerDir.x < 0) && playerDir.y == 0)
                {
                    anim.SetFloat("xMove", playerDir.x);
                    anim.SetFloat("yMove", 0);
                }
                if ((playerDir.y > 0 || playerDir.y < 0) && playerDir.x == 0)
                {
                    anim.SetFloat("yMove", playerDir.y);
                    anim.SetFloat("xMove", 0);
                }
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
        private IEnumerator AttackCoroutine()
        {
            SetAttackAnimations();
            yield return new WaitForSeconds(0.5f);
            states = PlayerStates.IDLE;
            anim.ResetTrigger("attack1");
        }
        public Stats GetStats()
        {
            return statList;
        }
    }
}
