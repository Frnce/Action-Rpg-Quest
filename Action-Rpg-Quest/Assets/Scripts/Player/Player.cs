﻿using System.Collections;
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
    public class Player : Entity, IDamageable
    {
        public static Player instance;

        [SerializeField]
        private float rollSpeed = 5.0f;
        [SerializeField]
        private float rollDistance = 0.1f;
        [SerializeField]
        private LayerMask blockingLayer = 0;

        private PlayerController playerControls = null;
        private Vector3 playerDir = Vector3.zero;
        private Vector3 playerLastDir = Vector3.zero;
        private CircleCollider2D myCollider = null;
        private PlayerStates states;
        private RaycastHit2D hit;
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
            rb2d = GetComponent<Rigidbody2D>();
            playerControls = PlayerController.instance;
            states = PlayerStates.IDLE;
            myCollider = GetComponent<CircleCollider2D>();
        }
        // Update is called once per frame
        void Update()
        {
            playerDir = playerControls.GetMovement;
            hit = Physics2D.CircleCast(transform.TransformPoint(myCollider.offset), myCollider.radius, playerDir, myCollider.radius, blockingLayer);
            if (states != PlayerStates.ROLLING)
            {
                if (playerControls.GetAttackKey)
                {
                    states = PlayerStates.ATTACKING;
                    StartCoroutine(AttackCoroutine(playerLastDir));
                }
                if (playerControls.GetDodgeKey)
                {
                    if (playerDir != Vector3.zero)
                    {
                        states = PlayerStates.ROLLING;
                        StartCoroutine(DodgeRoll(rollSpeed, rollDistance));
                    }
                }
            }

            //if (Input.GetKeyDown(KeyCode.Alpha9)) // For Screenshoting stuff
            //{
            //    ScreenCapture.CaptureScreenshot("SomeLevel");
            //}
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
            rb2d.velocity = new Vector2(playerDir.x * rollSpeed, playerDir.y * rollSpeed);
            yield return new WaitForSeconds(rollTime);
            states = PlayerStates.IDLE;
            rb2d.velocity = Vector3.zero;
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
                if(hit.collider == null)
                {
                    rb2d.MovePosition(transform.position + playerDir * movementSpeed * Time.deltaTime);
                }
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
        private IEnumerator AttackCoroutine(Vector3 direction)
        {
            SetAttackAnimations();
            rb2d.velocity = Vector3.zero;
            yield return new WaitForSeconds(0.5f);
            states = PlayerStates.IDLE;
            anim.ResetTrigger("attack1");
        }

        public Stats GetStats
        {
            get
            {
                return statList;
            }
            set
            {
                statList = value;
            }
        }

        public void TakeDamage(int damage)
        {
            //take damage
            // Invincible for 0.5f
            //animate damage
            health -= damage;
            Debug.Log(gameObject.name + "| HP : " + health + " | DAmage : " + damage);
        }
    }
}
