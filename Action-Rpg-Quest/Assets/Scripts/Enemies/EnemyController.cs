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
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}