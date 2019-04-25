using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Controller
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;
        private void Awake()
        {
            instance = this;
        }
        private int xMove;
        private int yMove;
        private bool interactKey;
        private bool attackKey;
        private bool dodgeKey;
        private bool openMenuKey;
        private void Start()
        {
            Keys();
        }
        void Update()
        {
            Keys();
        }
        private void Keys()
        {
            xMove = (int)Input.GetAxisRaw("Horizontal");
            yMove = (int)Input.GetAxisRaw("Vertical");
            attackKey = Input.GetButtonDown("Fire1");
            dodgeKey = Input.GetButtonDown("Fire2");
            openMenuKey = Input.GetButtonDown("OpenMenu");
            interactKey = Input.GetButtonDown("Interact");
        }
        public Vector2 GetMovement
        {
            get
            {
                return new Vector2(xMove, yMove);
            }
        }
        public bool GetAttackKey
        {
            get
            {
                return attackKey;
            }
        }
        public bool GetDodgeKey
        {
            get
            {
                return dodgeKey;
            }
        }
        public bool GetOpenMenuKey
        {
            get
            {
                return openMenuKey;
            }
        }
        public bool GetInteractKey
        {
            get
            {
                return interactKey;
            }
        }
    }
}