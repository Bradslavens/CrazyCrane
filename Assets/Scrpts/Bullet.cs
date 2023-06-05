using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Deactivate this bullet on collision
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Deactivate this bullet when entering a trigger
        gameObject.SetActive(false);
    }
}
