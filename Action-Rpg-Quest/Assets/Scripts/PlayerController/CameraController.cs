using Advent.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Controller
{
    public class CameraController : MonoBehaviour
    {
        private Player player;
        private Vector3 target, mousePos, refvel;
        [SerializeField]
        private float cameraDis = 3.5f;
        [SerializeField]
        private float smoothTime = 0.2f;
        private float zstart;

        void Start()
        {
            player = Player.instance;
            target = player.transform.position;
            zstart = transform.position.z;
        }

        // Update is called once per frame
        void Update()
        {
            mousePos = CaptureMousePos();
            target = UpdateTargetPos();
            UpdateCameraPosition();
        }
        private void UpdateCameraPosition()
        {
            Vector3 tempPos;
            tempPos = Vector3.SmoothDamp(transform.position, target, ref refvel, smoothTime);
            transform.position = tempPos;
        }

        private Vector3 UpdateTargetPos()
        {
            Vector3 mouseOffset = mousePos * cameraDis;
            Vector3 ret = player.transform.position + mouseOffset;
            ret.z = zstart;
            return ret;
        }

        private Vector3 CaptureMousePos()
        {
            Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            ret *= 2;
            ret -= Vector2.one;
            float max = 0.9f;
            if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
            {
                ret = ret.normalized;
            }
            return ret;
        }
    }
}