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
        private List<GameObject> currentEnemySpawned = new List<GameObject>();
        [SerializeField]
        private int enemySpawnCount = 0;
        [SerializeField]
        private float spawnRadius = 0;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(currentEnemySpawned.Count <= 0)
            {
                SpawnEnemies();
            }
            else
            {
                return;
            }
        }

        private void SpawnEnemies()
        {
            for (int i = 0; i < enemySpawnCount; i++)
            {
                StartCoroutine(SpawnEnemyCoroutine());
            }
        }
        private IEnumerator SpawnEnemyCoroutine()
        {
            int randomEnemy = Random.Range(0, enemyList.Count);
            Instantiate(enemyList[randomEnemy], GetRandomPosition(), Quaternion.identity, transform);
            currentEnemySpawned.Add(enemyList[randomEnemy]);
            yield return new WaitForSeconds(1f);
        }
        private Vector2 GetRandomPosition()
        {
            return (Random.insideUnitCircle * spawnRadius) + (Vector2)transform.position;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
        public void RemoveFromEnemyList(GameObject obj)
        {
            currentEnemySpawned.Remove(obj);
        }
    }
}