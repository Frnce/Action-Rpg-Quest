using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Player
{
    public class PlayerMovementScript : MonoBehaviour
    {
        public float movementSpeed = 0f;
        public Transform playerSpriteRenderer = null;

        private Vector3 playerDir = Vector3.zero;

        private bool isFacingRight;
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
            playerControls.GetRb2d.MovePosition(transform.position + playerDir * movementSpeed * Time.deltaTime);
        }
        private void Flip()
        {
            isFacingRight = !isFacingRight;

            Vector3 theScale = playerSpriteRenderer.localScale;
            theScale.x *= -1;
            playerSpriteRenderer.localScale = theScale;
        }
    }
}
