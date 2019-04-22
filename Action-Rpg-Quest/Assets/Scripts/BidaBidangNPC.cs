using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.NPC
{
    public class BidaBidangNPC : MonoBehaviour
    {
        bool isTalkable = false;
        private void Update()
        {
            if (isTalkable)
            {
                Debug.Log("talkable");
            }
            if (isTalkable && Input.GetButtonDown("Fire1"))
            {
                Debug.Log("TALKING....");
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isTalkable = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isTalkable = false;
            }
        }
    }
}