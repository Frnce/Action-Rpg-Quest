using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Controller;

namespace Advent.Player
{
    public class Player : MonoBehaviour
    {
        public static Player instance;

        [SerializeField]
        private float movementSpeed = 10f;
        private bool isMoving = true;
        private Rigidbody2D rb2d;
        private Animator anim;
        private PlayerController playerControls = null;
        private Vector3 playerDir = Vector3.zero;
        private void Awake()
        {
            if(instance == null)
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
        void Start()
        {
            playerControls = PlayerController.instance;
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }
        // Update is called once per frame
        void Update()
        {
            playerDir.x = playerControls.GetXMovement();
            playerDir.y = playerControls.GetYMovement();
            SetDirectionAnimations();
        }
        private void FixedUpdate()
        {
            Movement();
        }
        private void Movement()
        {
            if (playerDir.x != 0 || playerDir.y != 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            rb2d.velocity = new Vector2(Mathf.Lerp(0, playerDir.x * movementSpeed, 0.8f), //Change MovementSpeed to something from stats
                                               Mathf.Lerp(0, playerDir.y * movementSpeed, 0.8f));
        }
        private void SetDirectionAnimations()
        {
            if(playerDir != Vector3.zero)
            {
                if((playerDir.x > 0 || playerDir.x < 0) && playerDir.y == 0)
                {
                    anim.SetFloat("xMove", playerDir.x);
                    anim.SetFloat("yMove", 0);
                }
                if ((playerDir.y > 0 || playerDir.y < 0) && playerDir.x == 0)
                {
                    anim.SetFloat("yMove", playerDir.y);
                    anim.SetFloat("xMove", 0);
                }
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
    }
}
