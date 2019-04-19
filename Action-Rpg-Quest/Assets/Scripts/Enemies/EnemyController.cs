using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Utilities;
using Advent.Interfaces;
using Advent.AI;

namespace Advent.Entities
{
    [RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
    public class EnemyController : Entity , IDamageable
    {
        public float radius = 5f;
        private Vector2 startingPosition; //Change to Spawn Position

        private GameObject target;
        private Vector3 randomPosition;

        [SerializeField]
        private LayerMask blockingLayer = 0;
        private CircleCollider2D myCollider;
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            startingPosition = transform.position;
            SetRandomPosition();

            anim.SetBool("isMoving", true);
            anim.SetFloat("xMove", (randomPosition - transform.position).x);
            anim.SetFloat("yMove", (randomPosition - transform.position).y);

            target = Player.instance.gameObject;
            myCollider = GetComponent<CircleCollider2D>();
        }
        public void Movement(Vector3 direction)
        {
            Vector2 targetDirection = target.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.CircleCast(transform.TransformPoint(myCollider.offset), myCollider.radius, targetDirection, myCollider.radius, blockingLayer);
            if (hit.collider == null)
            {
                rb2d.MovePosition(direction);
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
        public void TakeDamage(int damage)
        {
            //take damage
            //small knockback or stagger . not moving for 0.3f
            StartCoroutine(TakeDamageCour());
        }
        IEnumerator TakeDamageCour()
        {
            GetComponent<StateController>().isAiActive = false;
            //staggerHere
            //knockback
            yield return new WaitForSeconds(0.3f);
            GetComponent<StateController>().isAiActive = true;
        }
    }

}