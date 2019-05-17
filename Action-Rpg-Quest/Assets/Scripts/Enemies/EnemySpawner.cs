using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> enemyList = new List<GameObject>();
        [SerializeField]
        private int maxEnemySpawns = 0;
        [SerializeField]
        private float spawnRadius = 0;
        [SerializeField]
        private float timeBetweenSpawn = 0;
        [SerializeField]
        private float respawnTime = 30f;
        // Update is called once per frame
        void Update()
        {
            if(transform.childCount >= maxEnemySpawns)
            {
                return;
            }
            SpawnEnemies();
        }
        private void SpawnEnemies()
        {
            StartCoroutine(SpawnEnemyCoroutine());
        }
        private IEnumerator RespanwRoutine()
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEnemies();
        }
        private IEnumerator SpawnEnemyCoroutine()
        {
            int randomEnemy = Random.Range(0, enemyList.Count);
            Instantiate(enemyList[randomEnemy], GetRandomPosition(), Quaternion.identity, transform);
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
        private Vector2 GetRandomPosition()
        {
            return (Random.insideUnitCircle * spawnRadius) + (Vector2)transform.position;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
    }
}