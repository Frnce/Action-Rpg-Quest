using Advent.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public class MeleeHitPlayer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //TODO Make layermask modular . using a variable
            if (collision.CompareTag("Player") && collision.gameObject.layer == LayerMask.NameToLayer("Hurtbox"))
            {
                //collision.GetComponentInParent<IDamageable>().TakeDamage(10,transform.localPosition);
            }
        }
    }
}