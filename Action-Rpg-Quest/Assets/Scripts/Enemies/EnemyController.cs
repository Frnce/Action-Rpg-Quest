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

        [SerializeField]
        private LayerMask blockingLayer;
        private CircleCollider2D myCollider;
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            startingPosition = transform.position;
            SetRandomPosition();
            prepareAttackTime = maxPrepareAttackTime;
            target = Player.instance.gameObject;
            myCollider = GetComponent<CircleCollider2D>();
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
        public void Movement(Vector3 direction)
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.TransformPoint(myCollider.offset), myCollider.radius, direction, myCollider.radius, blockingLayer);
            if (hit.collider == null)
            {
                rb2d.MovePosition(direction);
            }
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