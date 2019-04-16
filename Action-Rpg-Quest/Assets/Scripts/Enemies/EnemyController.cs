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
        private Vector2 startingPosition;
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            startingPosition = transform.position;
            Debug.Log(transform.position += (Vector3)(Random.insideUnitCircle * radius));
            transform.position += (Vector3)(Random.insideUnitCircle * radius);
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(startingPosition, radius);
        }
    }

}