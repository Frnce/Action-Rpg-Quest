using UnityEngine;
using System.Collections;

namespace Advent
{
    public class HitScript : MonoBehaviour
    {
        private void Start()
        {
            
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Enemies") && collision.gameObject.layer == LayerMask.NameToLayer("HurtBox"))
            {
                collision.GetComponentInParent<IDamageable>().TakeDamage(1f);
            }
        }
    }
}