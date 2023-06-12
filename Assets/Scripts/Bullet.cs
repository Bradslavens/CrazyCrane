using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damageAmount = 10; // You can set this to whatever value you want.

    void OnCollisionEnter(Collision collision)
    {
        // Deactivate this bullet on collision
        gameObject.SetActive(false);
        DealDamage(collision.collider);
    }

    void OnTriggerEnter(Collider other)
    {

        IDamageable myInterface = other.GetComponent<IDamageable>();
        if (myInterface != null)
        {
            Debug.Log("got damage d45");
        }


        // Deactivate this bullet when entering a trigger
        gameObject.SetActive(false);
        DealDamage(other);
    }

    void DealDamage(Collider collider)
    {

        // Try to get a IDamageable component on the collided object.
        var damageable = collider.GetComponent<IDamageable>();

        if (damageable == null)
        {
            Debug.Log("Failed to get IDamageable component from collided object.");
        }
        // If the object has a IDamageable component, call its DealDamage method.
        else if (damageable != null)
        {

            Debug.Log("got idamageable");
            damageable.DealDamage(damageAmount);
        }
    }
}
