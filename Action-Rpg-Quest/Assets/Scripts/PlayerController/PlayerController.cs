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
        //private readonly bool acceptKey;
        //private readonly bool cancelKey;
        //private readonly bool action1;
        //private readonly bool action2;
        //private readonly bool action3;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            Movement();
        }
        void Update()
        {
            Movement();
        }
        private void Movement()
        {
            xMove = (int)Input.GetAxisRaw("Horizontal");
            yMove = (int)Input.GetAxisRaw("Vertical");
        }

        public int GetXMovement()
        {
            return xMove;
        }
        public int GetYMovement()
        {
            return yMove;
        }
        //public bool GetAcceptKey()
        //{
        //    return acceptKey;
        //}
        //public bool GetCancelKey()
        //{
        //    return cancelKey;
        //}
    }
}