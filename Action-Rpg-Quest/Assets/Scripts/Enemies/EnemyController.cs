using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Utilities;
using Advent.Interfaces;
using Advent.AI;
using Advent.Items;
using Advent.Manager;
using TMPro;

namespace Advent.Entities
{
    [RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
    public class EnemyController : Entity , IDamageable
    {
        public float radius = 5f;
        private Vector2 startingPosition; //Change to Spawn Position

        private GameObject target;
        private Vector3 randomPosition;
        private Vector2 targetDirection;

        [SerializeField]
        private LayerMask blockingLayer = 0;
        [SerializeField]
        private GameObject customCollider = null;
        private CircleCollider2D myCollider;
        [Space]
        [SerializeField]
        private GameObject deathParticleEffect = null;
        [SerializeField]
        private AudioClip hurtAudio = null;
        [Space]
        [SerializeField]
        private GameObject hitPointsBar = null;
        [Space]
        [SerializeField]
        private float maxDeathTime = 0;
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            startingPosition = transform.position;
            SetRandomPosition();

            anim.SetBool("isMoving", true);

            target = Player.instance.gameObject;
            myCollider = GetComponent<CircleCollider2D>();

            hitPointsBar.SetActive(false);

            InitAttributes();
            InitMovementSpeed();

            InitBaseDamage(statList.baseSTR, statList.bonusSTR.getValue, currentLevel);
        }
        public void Movement(Vector3 direction)
        {
            targetDirection = target.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.CircleCast(transform.TransformPoint(myCollider.offset), myCollider.radius, targetDirection, myCollider.radius, blockingLayer);
            if (hit.collider == null)
            {
                rb2d.MovePosition(direction);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
        public void SetRandomPosition()
        {
            randomPosition = (Random.insideUnitCircle * radius) + startingPosition;
        }
        public Vector3 GetRandomPosition()
        {
            return randomPosition;
        }
        public GameObject GetTarget()
        {
            return target;
        }
        public Animator GetAnimator()
        {
            return anim;
        }
        public float GetMovementSpeed()
        {
            return entityStats.movementSpeed;
        }
        public override void Die()
        {
            if (currentHP <= 0)
            {
                if (entityStats.loot != null)
                {
                    FindObjectOfType<LootScript>().DropLoot(entityStats.loot,entityStats.dropChance,transform.position);
                }
                myCollider.enabled = false;
                customCollider.SetActive(false);
                hurtBox.SetActive(false);
                hitPointsBar.SetActive(false);
                anim.SetBool("isDead", true);
                StartCoroutine(DeathRoutine());
                //respawn
                //base.Die();
            }
        }
        public void TakeDamage(int damage,Vector3 targetPoint)
        {
            //Show HP bar when hit
            if (!hitPointsBar.activeSelf)
            {
                hitPointsBar.SetActive(true);
            }
            //Show a small particle effect when Hit
            GameObject GO = Instantiate(deathParticleEffect, transform);
            Destroy(GO, 0.5f); // Destroy the particle
            GameManager.instance.Freeze(); // Freezes game for a millisecond to show effects
            GameManager.instance.ShakeCamera();
            currentHP -= damage;
            if (floatingDamageText != null)
            {
                ShowFloatingDamageText(damage);
            }
            Debug.Log("HP : " + currentHP + " | DAmaged : " + damage);
            StartCoroutine(TakeDamageRoutine());
            Die();
        }
        private IEnumerator TakeDamageRoutine()
        {
            SoundManager.instance.EnemyHitSingleSfx(hurtAudio);
            sprite.color = Color.red;
            GetComponent<StateController>().isAiActive = false;
            rb2d.velocity = new Vector2(targetDirection.x * -knockbackDistance, targetDirection.y * -knockbackDistance); //knockback
            yield return new WaitForSeconds(0.05f);
            rb2d.velocity = Vector2.zero;
            yield return new WaitForSeconds(0.3f);
            sprite.color = Color.white;
            if (currentHP > 0)
            {
                GetComponent<StateController>().isAiActive = true;
            }
        }
        private IEnumerator DeathRoutine()
        {
            PlayerLevelManager.instance.GainExp(entityStats.expGiven);
            yield return new WaitForSeconds(maxDeathTime);
            anim.SetTrigger("deathFade");
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }

}