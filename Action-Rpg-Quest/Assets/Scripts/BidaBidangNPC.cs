using Advent.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.NPC
{
    public class BidaBidangNPC : MonoBehaviour , IInteractable
    {
        public void Interact()
        {
            Debug.Log("Interactive NPC");
        }
    }
}