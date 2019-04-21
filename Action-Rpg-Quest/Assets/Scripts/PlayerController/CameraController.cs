using Advent.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Controller
{
    public class CameraController : MonoBehaviour
    {
        public float smoothTime = 0.3f;
        private Transform target;
        private void Start()
        {
            target = Player.instance.transform;
        }
        void LateUpdate()
        {
            if(transform.position != target.position)
            {
                Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10);
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime);
            }
        }
    }
}