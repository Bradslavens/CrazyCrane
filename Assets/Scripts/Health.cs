using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public int health = 100;


    public void DealDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            EnemyManager enemyManager = GameObject.FindObjectOfType<EnemyManager>();
            enemyManager.RemoveEnemy(gameObject);
            // Handle death here.
            Destroy(gameObject);


        }
    }
}
