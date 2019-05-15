﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Utilities;
using Advent.Interfaces;
using Advent.AI;
using Advent.Items;
using Advent.Manager;

namespace Advent.Entities
{
    [RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
    public class EnemyController : Entity , IDamageable
    {
        public float knockbackDistance;
        public float radius = 5f;
        private Vector2 startingPosition; //Change to Spawn Position
        public SpriteRenderer sprite;

        private GameObject target;
        private Vector3 randomPosition;
        private Vector2 targetDirection;

        [SerializeField]
        private LayerMask blockingLayer = 0;
        private CircleCollider2D myCollider;
        [Space]
        [SerializeField]
        private GameObject deathParticleEffect;
        [Space]
        [SerializeField]
        private GameObject hitPointsBar;
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
            return movementSpeed;
        }
        public override void Die()
        {
            if (currentHP <= 0)
            {
                if(GetComponent<LootScript>() != null)
                {
                    GetComponent<LootScript>().DropLoot();
                }
                base.Die();
                //gameObject.SetActive(false);
            }
        }
        public void TakeDamage(int damage)
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
            Debug.Log("HP : " + currentHP + " | DAmaged : " + damage);
            StartCoroutine(TakeDamageCour());
            Die();
        }
        IEnumerator TakeDamageCour()
        {
            sprite.color = Color.red;
            GetComponent<StateController>().isAiActive = false;
            rb2d.velocity = new Vector2(targetDirection.x * -knockbackDistance, targetDirection.y * -knockbackDistance); //knockback
            yield return new WaitForSeconds(0.05f);
            rb2d.velocity = Vector2.zero;
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.3f);
            GetComponent<StateController>().isAiActive = true;
        }
    }

}