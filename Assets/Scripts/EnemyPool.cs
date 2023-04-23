using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize = 100;

    private List<GameObject> pool;

    void Start()
    {
        pool = new List<GameObject>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            pool.Add(enemy);
        }
    }

    public GameObject GetPooledEnemy()
    {
        foreach (GameObject enemy in pool)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }

        // If no inactive enemy is available, create a new one and add it to the pool
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.SetActive(false);
        pool.Add(newEnemy);
        return newEnemy;
    }
}
