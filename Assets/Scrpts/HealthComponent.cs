using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    // public variable for health
    public int health = 100;

    // Method to reduce health
    public void TakeDamage(int damage)
    {
        health -= damage;

        // Check if health is less than or equal to zero
        if (health <= 0)
        {
            // Destroy the game object this component is attached to
            Destroy(gameObject);
        }
    }
}
