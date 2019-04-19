using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Controller
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;
        private int xMove;
        private int yMove;
        private bool attackKey;
        private bool dodgeKey;
        private void Awake()
        {
            instance = this;
        }
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
        }

        public int GetXMovement()
        {
            return xMove;
        }
        public int GetYMovement()
        {
            return yMove;
        }
        public bool GetAttackKey()
        {
            return attackKey;
        }
        public bool GetDodgeKey()
        {
            return dodgeKey;
        }
    }
}