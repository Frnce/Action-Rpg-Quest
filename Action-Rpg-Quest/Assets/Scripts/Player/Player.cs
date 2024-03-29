﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Controller;
using Advent.Interfaces;
using Advent.Utilities;
using Advent.Manager;
using Advent.Items;

namespace Advent.Entities
{
    public enum PlayerStates
    {
        IDLE,
        MOVING,
        ATTACKING,
        ROLLING,
        INMENU
    }
    public class Player : Entity, IDamageable
    {
        public static Player instance;

        [SerializeField]
        private float rollSpeed = 5.0f;
        [SerializeField]
        private float rollDistance = 0.1f;
        [SerializeField]
        private Transform avatarRenderer = null;
        [Space]
        public AudioClip walkSound;
        [Space]
        [SerializeField]
        private CircleCollider2D myCollider = null;
        [SerializeField]
        private LayerMask blockingLayer = 0;
        private PlayerController playerControls = null;
        private Vector3 playerDir = Vector3.zero;
        private Vector3 playerLastDir = Vector3.zero;
        private PlayerStates states;
        private RaycastHit2D hit;
        private bool isNearInteractable = false;
        private GameObject collidedObject; //saves the data for the collided object;
        private bool isFacingRight;
        private float timeBetweenAttack;
        private CameraController cam;

        private bool canBeHit = true;

        public GameObject weapon;
        public TrailRenderer weaponTrail;
        public ParticleSystem dustWalkingEffect = null;
        public float startTimeBetweenAttack;

        public bool IsAttacking{ get; set; }

        private void Awake()
        {
            if (instance == null)
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
            cam = FindObjectOfType<CameraController>();
            playerControls = PlayerController.instance;
            states = PlayerStates.IDLE;

            entitiesStats = new EntitiesStats(entityStats.strength, entityStats.dexterity, entityStats.vitality, entityStats.intelligence);
            //weaponTrail.emitting = false;

            //EquipmentManager.instance.onEquipmentChangedCallback += onEquipmentChange;

            timeBetweenAttack = startTimeBetweenAttack;

            //EquipmentManager.instance.EquipDefaults();

            //InitAttributes();
            //InitMovementSpeed();

            //InitHP();
            //InitMP();

            //InitDamage();
            //InitPhysicalDefense();
        }
        // Update is called once per frame
        void Update()
        {
            if (states != PlayerStates.INMENU)
            {
                playerDir = playerControls.GetMovement;
                hit = Physics2D.CircleCast(transform.TransformPoint(myCollider.offset), myCollider.radius, playerDir, myCollider.radius, blockingLayer);
                if (states != PlayerStates.ROLLING)
                {
                    PlayerDodge();
                }
                InteractObject();
                if (!IsAttacking)
                {
                    FlipCharacter();
                }
            }
            //TODO fix Trail Effect
            if(states == PlayerStates.MOVING)
            {
                if (!dustWalkingEffect.isPlaying)
                {
                    dustWalkingEffect.Play();
                }
            }
        }
        private void FixedUpdate()
        {
            if (states != PlayerStates.ROLLING || states != PlayerStates.INMENU)
            {
                if (canBeHit)
                {
                    Movement();
                }
            }
        }
        private void FlipCharacter()
        {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mouse.x > transform.position.x && isFacingRight)
            {
                Flip();
            }
            else if (mouse.x < transform.position.x && !isFacingRight)
            {
                Flip();
            }
        }
        private void Flip()
        {
            isFacingRight = !isFacingRight;

            Vector3 theScale = avatarRenderer.localScale;
            theScale.x *= -1;
            avatarRenderer.localScale = theScale;
        }
        private void PlayerDodge()
        {
            if (playerControls.GetDodgeKey)
            {
                if (playerDir != Vector3.zero)
                {
                    states = PlayerStates.ROLLING;
                    StartCoroutine(DodgeRoll(rollSpeed, rollDistance));
                }
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
                if (playerDir.x != 0 || playerDir.y != 0)
                {
                    states = PlayerStates.MOVING;
                    anim.SetBool("isMoving", true); 
                }
                else
                {
                    states = PlayerStates.IDLE;
                    anim.SetBool("isMoving", false);
                }
                if (hit.collider == null)
                {
                    rb2d.MovePosition(transform.position + playerDir * entityStats.movementSpeed * Time.deltaTime);
                }
            }
        }
        private void InteractObject()
        {
            if (isNearInteractable && collidedObject != null && playerControls.GetInteractKey)
            {
                Debug.Log(collidedObject.name);
                collidedObject.GetComponent<IInteractable>().Interact();
            }
        }
        private void SetAttackAnimations()
        {
            anim.SetTrigger("attack1");
        }
        //TODO problem : the farther the mouse position on the screen , the longer the player step
        private Vector2 MicrosteponAttack()
        {
            Vector3 stepDirection;
            stepDirection = Input.mousePosition;
            stepDirection.z = 0.0f;
            stepDirection = Camera.main.ScreenToWorldPoint(stepDirection);
            return (stepDirection - transform.position).normalized;
        }
        //private void InitDamage()
        //{
        //    InitBaseDamage(statList.baseSTR, statList.bonusSTR.getValue,statList.weaponDamage, currentLevel);
        //}
        //private void InitPhysicalDefense()
        //{
        //    InitPhysicalDefense(statList.baseSTR, statList.armorDefense.getValue);
        //}
        //private void InitHP()
        //{
        //    InitHitpoints(statList.baseVIT, statList.bonusVIT.getValue, currentLevel);
        //}
        //private void InitMP()
        //{
        //    InitManaPoints(statList.baseINT, statList.bonusINT.getValue, currentLevel);
        //}
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null)
            {
                Debug.Log(collision.name);
                isNearInteractable = true;
                collidedObject = collision.gameObject;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            isNearInteractable = false;
            collidedObject = null;
        }
        public void SetPlayerStates(PlayerStates states)
        {
            this.states = states;
        }
        public SpriteRenderer PlayerSprite
        {
            get
            {
                return avatarRenderer.GetComponent<SpriteRenderer>();
            }
        }
        public EntitiesStats GetPlayerStats()
        {
            return entitiesStats;
        }
        public PlayerStates GetPlayerStates
        {
            get
            {
                return states;
            }
        }
        public void MicroStepAction()
        {
            rb2d.velocity = Vector3.zero;
            rb2d.velocity += MicrosteponAttack();
        }
        public void TakeDamage(int damage,Vector3 targetPoint)
        {
            if (canBeHit)
            {
                //take damage
                // Invincible for 0.5f
                //animate damage
                currentHP -= damage;
                if (floatingDamageText != null)
                {
                    ShowFloatingDamageText(damage);
                }
                Debug.Log("HP : " + currentHP + " | DAmaged : " + damage);

                Vector3 knockbackDirection = targetPoint - transform.position;
                StartCoroutine(TakeDamageRoutine(knockbackDirection));
            }
        }
        private IEnumerator TakeDamageRoutine(Vector3 targetPoint)
        {
            canBeHit = false;
            //SoundManager.instance.EnemyHitSingleSfx(hurtAudio);
            sprite.color = Color.red;
            rb2d.velocity = Vector3.zero;
            rb2d.velocity = new Vector2(targetPoint.x * -knockbackDistance, targetPoint.y * -knockbackDistance);
            yield return new WaitForSeconds(0.05f);
            rb2d.velocity = Vector2.zero;
            canBeHit = true;
            sprite.color = Color.white;
        }
    }
}
