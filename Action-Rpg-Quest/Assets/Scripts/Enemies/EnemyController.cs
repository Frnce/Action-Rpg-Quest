using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Utilities;
using Advent.Interfaces;

namespace Advent.Entities
{
    [RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
    public class EnemyController : Entity
    {
        public float radius = 5f;
        public float maxIdleTime = 1f;
        private float idleTime;
        private Vector2 startingPosition;
        private Vector3 randomPosition;
        private bool onPatrol = false;

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            startingPosition = transform.position;
            GetRandomPosition();
            idleTime = maxIdleTime;
            onPatrol = true;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }
        private void Move()
        {
            if (onPatrol)
            {
                if (transform.position == randomPosition)
                {
                    if (idleTime <= 0)
                    {
                        GetRandomPosition();
                        Vector2 newDirection = randomPosition - transform.position;
                        anim.SetBool("isMoving", true);
                        SetMovementAnimation(newDirection);

                        idleTime = maxIdleTime;
                    }
                    else
                    {
                        anim.SetBool("isMoving", false);
                        idleTime -= Time.deltaTime;
                    }
                }
                else
                {
                    Vector2 direction = Vector2.MoveTowards(transform.position, randomPosition, movementSpeed * Time.deltaTime);
                    rb2d.MovePosition(direction);
                }
            }
        }
        private void SetMovementAnimation(Vector2 newDirection)
        {
            anim.SetFloat("xMove", newDirection.x);
            anim.SetFloat("yMove", newDirection.y);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(startingPosition, radius);
        }
        private void GetRandomPosition()
        {
            randomPosition = (Random.insideUnitCircle * radius) + startingPosition;
        }
        private Vector2 ToGridDirection(Vector2 vector)
        {
            float x = vector.x == 0 ? 0 : vector.x / (Mathf.Abs(vector.x));
            float y = vector.y == 0 ? 0 : vector.y / (Mathf.Abs(vector.y));

            return new Vector2(x, y);
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            onPatrol = false;
            Debug.Log(collision.collider.name);
            if (collision.collider.CompareTag("Player"))
            {
                rb2d.velocity = Vector3.zero;
                rb2d.isKinematic = true;
            }
            else if (collision.collider.CompareTag("Walls"))
            {
                GetRandomPosition();
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            onPatrol = true;
            rb2d.isKinematic = false;
        }
    }

}