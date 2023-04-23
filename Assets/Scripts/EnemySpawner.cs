using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyPool enemyPool;
    public int numberOfEnemiesToSpawn = 100;
    public float spawnRadius = 10f;
    public float spawnInterval = 1f;

    private float timeSinceLastSpawn;

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = enemyPool.GetPooledEnemy();
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 0f; // Adjust this if your enemies should spawn at a different height

        enemy.transform.position = spawnPosition;
        enemy.SetActive(true);
    }
}
