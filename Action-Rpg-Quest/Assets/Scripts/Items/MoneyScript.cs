using Advent.Entities;
using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    public class MoneyScript : MonoBehaviour
    {
        [SerializeField]
        private int moneyValue = 0;
        [Space]
        [Header("Follow Player")]
        [SerializeField]
        private float minFollowModifier = 10f;
        [SerializeField]
        private float maxFollowModifer = 11f;
        [SerializeField]
        private float toFollowRadius = 5f;
        [SerializeField]
        private LayerMask playerLayer;

        private Vector3 _velocity = Vector3.zero;
        private bool isFollowing = false;

        private InventoryManager inventoryManager = null;
        // Start is called before the first frame update
        void Start()
        {
            inventoryManager = InventoryManager.instance;
        }
        private void Update()
        {
            if (Physics2D.OverlapCircle(transform.position, toFollowRadius, playerLayer))
            {
                isFollowing = true;
            }

            if (isFollowing)
            {
                transform.position = Vector3.SmoothDamp(transform.position, Player.instance.transform.position, ref _velocity, Time.deltaTime * Random.Range(minFollowModifier, maxFollowModifer));
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                inventoryManager.UseMoney(moneyValue,false);
                Destroy(gameObject);
            }
        }
    }
}
