using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;  // The damage property

    void OnCollisionEnter(Collision collision)
    {
        // Deactivate this bullet on collision
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the other object is tagged "Enemy"
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Get the health component
            var healthComponent = other.gameObject.GetComponent<HealthComponent>();

            // If the enemy has a Health component
            if (healthComponent != null)
            {
                // Call the TakeDamage method
                healthComponent.TakeDamage(damage);
            }
        }
        else
        {
            // Deactivate this bullet when entering a non-enemy trigger
            gameObject.SetActive(false);
        }
    }
}
