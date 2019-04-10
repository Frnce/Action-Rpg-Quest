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
        private PlayerController playerControls = null;
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
        }
        // Update is called once per frame
        void Update()
        {

        }
        private void FixedUpdate()
        {
            Movement();
        }
        private void Movement()
        {
            int xDir = playerControls.GetXMovement();
            int yDir = playerControls.GetYMovement();
            if (xDir != 0 || yDir != 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            rb2d.velocity = new Vector2(Mathf.Lerp(0, xDir * movementSpeed, 0.8f), //Change MovementSpeed to something from stats
                                               Mathf.Lerp(0, yDir * movementSpeed, 0.8f));
        }
    }
}
