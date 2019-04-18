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
        // Move
        public float radius = 5f;
        private Vector2 startingPosition;

        // Attack
        public float maxPrepareAttackTime = 1f;
        private float prepareAttackTime;
        private GameObject target;
        private Vector3 randomPosition;
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            startingPosition = transform.position;
            SetRandomPosition();
            prepareAttackTime = maxPrepareAttackTime;
            target = Player.instance.gameObject;
        }
        public void SetMovementAnimation(Vector2 newDirection)
        {
            anim.SetFloat("xMove", newDirection.x);
            anim.SetFloat("yMove", newDirection.y);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(startingPosition, radius);
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
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                Debug.Log("asd");
                rb2d.velocity = Vector3.zero;
                rb2d.isKinematic = true;
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            rb2d.isKinematic = false;
        }
        public void Movement(Vector3 direction)
        {
            rb2d.MovePosition(direction);
        }
        public float GetMovement()
        {
            return movementSpeed;
        }
        public Animator GetAnimator()
        {
            return anim;
        }
    }

}