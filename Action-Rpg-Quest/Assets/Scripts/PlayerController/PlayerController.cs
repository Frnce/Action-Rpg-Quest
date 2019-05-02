﻿using System.Collections;
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
        public bool GetAttackKey { get; private set; }
        public bool GetDodgeKey { get; private set; }
        public bool GetOpenMenuKey { get; private set; }
        public bool GetInteractKey { get; private set; }
        public Vector2 GetMousePosition { get; private set; }

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
            GetAttackKey = Input.GetButtonDown("Fire1");
            GetDodgeKey = Input.GetButtonDown("Fire2");
            GetOpenMenuKey = Input.GetButtonDown("OpenMenu");
            GetInteractKey = Input.GetButtonDown("Interact");

            GetMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        public Vector2 GetMovement
        {
            get
            {
                return new Vector2(xMove, yMove);
            }
        }
    }
}