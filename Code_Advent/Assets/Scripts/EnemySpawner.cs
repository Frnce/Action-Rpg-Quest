using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyObject = null;

    public float spawnRadius = 5f;

    public float startSpawnTime = 1f;
    private float spawnTime;

    bool isEnemyAlive = false;
    private GameObject spawnedObject;

    private void Start()
    {
        spawnTime = startSpawnTime;
        SpawnEnemy();
    }
    private void Update()
    {
        if (!isEnemyAlive)
        {
            if(spawnTime <= 0)
            {
                SpawnEnemy();
                spawnTime = startSpawnTime;
            }
            else
            {
                spawnTime -= Time.deltaTime;
            }
        }
    }
    private void CheckEnemyIsAlive()
    {
        if(gameObject.transform.childCount == 0)
        {
            isEnemyAlive = false;
        }
    }
    private void SpawnEnemy()
    {
        spawnedObject = Instantiate(enemyObject,(Random.insideUnitCircle * spawnRadius) + (Vector2)transform.position ,Quaternion.identity ,gameObject.transform);
        isEnemyAlive = true;
    }
}
