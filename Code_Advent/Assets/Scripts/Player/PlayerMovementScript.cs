using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Player
{
    public class PlayerMovementScript : MonoBehaviour
    {
        public float movementSpeed = 0f;
        [SerializeField]
        private CircleCollider2D myCollider = null;
        [SerializeField]
        private LayerMask blockingLayer = 0;
        private RaycastHit2D hit;
        private Vector3 playerDir = Vector3.zero;

        private bool isFacingRight = true;
        private bool isMoving = false;

        private PlayerControlsScript playerControls;

        private void Start()
        {
            playerControls = PlayerControlsScript.instance;
        }
        // Update is called once per frame
        void Update()
        {
            playerDir = playerControls.GetMovement;
            hit = Physics2D.CircleCast(transform.TransformPoint(myCollider.offset), myCollider.radius, playerDir, myCollider.radius, blockingLayer);


            if (playerDir.x != 0 || playerDir.y != 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            InitAnimations();
        }
        private void FixedUpdate()
        {
            Movement();
        }
        private void InitAnimations()
        {
           playerControls.GetAnim.SetBool("isMoving", isMoving);
        }
        private void Movement()
        {
            if (playerDir.x > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (playerDir.x < 0 && isFacingRight)
            {
                Flip();
            }
            if(hit.collider != null)
            {
                Debug.Log(hit.collider.name);
            }
            if (hit.collider == null)
            {
                playerControls.GetRb2d.MovePosition(Vector2.Lerp(transform.position, transform.position + playerDir * movementSpeed, Time.fixedDeltaTime));
            }
        }
        private void Flip()
        {
            isFacingRight = !isFacingRight;

            Vector3 theScale = playerControls.GetSpriteRenderer.localScale;
            theScale.x *= -1;
            playerControls.GetSpriteRenderer.localScale = theScale;
        }
            
        private void OnDrawGizmos()
        {

        }
    }
}
