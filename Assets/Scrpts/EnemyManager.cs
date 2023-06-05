using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> currentEnemies = new List<GameObject>();

    public void AddEnemy(GameObject enemy)
    {
        currentEnemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        currentEnemies.Remove(enemy);
    }
}
