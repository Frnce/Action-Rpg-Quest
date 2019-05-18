using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Controller;
using Advent.Interfaces;
using Advent.Utilities;
using Advent.Manager;

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
        [Space]
        [Header("Audio")]
        public AudioClip[] swordSwings;
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
        private Vector2 mouse;

        private bool canBeHit = true;

        public GameObject weapon;
        public TrailRenderer weaponTrail;
        public ParticleSystem dustWalkingEffect = null;
        public float startTimeBetweenAttack;
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

            weaponTrail.emitting = false;

            timeBetweenAttack = startTimeBetweenAttack;
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
            }
            if (states != PlayerStates.ATTACKING)
            {
                FlipCharacter();
                Aim();
            }
            Debug.Log(states);
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

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
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
                    rb2d.MovePosition(transform.position + playerDir * movementSpeed * Time.deltaTime);
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
        private void Aim()
        {
            //The Weapon should be facing up or vector2.up for it to work properly
            mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);        //Mouse position
            Vector3 objpos = Camera.main.WorldToViewportPoint(weapon.transform.position);        //Object position on screen
            Vector2 relobjpos = new Vector2(objpos.x - 0.5f, objpos.y - 0.5f);            //Set coordinates relative to object
            Vector2 relmousepos = new Vector2(mouse.x - 0.5f, mouse.y - 0.5f) - relobjpos;
            float angle = Vector2.Angle(Vector2.up, relmousepos);    //Angle calculation
            if (relmousepos.x > 0)
                angle = 360 - angle;
            Quaternion quat = Quaternion.identity;
            quat.eulerAngles = new Vector3(0, 0, angle); //Changing angle
            weapon.transform.rotation = quat;
        }
        public void PlayerAttack()
        {
            if (states != PlayerStates.ROLLING)
            {
                states = PlayerStates.ATTACKING;
                SoundManager.instance.PlayerAttackRandomizeSfx(swordSwings);
                StartCoroutine(AttackCoroutine(playerLastDir));
                timeBetweenAttack = startTimeBetweenAttack;
            }
        }
        private IEnumerator AttackCoroutine(Vector3 direction)
        {
            SetAttackAnimations();
            weaponTrail.emitting = true;
            rb2d.velocity = Vector3.zero;
            rb2d.velocity += MicrosteponAttack();
            yield return new WaitForSeconds(0.2f);
            weaponTrail.emitting = false;
            states = PlayerStates.IDLE;
            anim.ResetTrigger("attack1");
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
        public void SetPlayerStates(PlayerStates states)
        {
            this.states = states;
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
