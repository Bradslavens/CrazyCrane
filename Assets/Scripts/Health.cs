using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public int health = 100;

    public void DealDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            // Handle death here.
            Destroy(gameObject);
        }
    }
}
