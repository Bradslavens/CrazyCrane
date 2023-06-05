using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public int health = 100;  // set the starting health

    private void OnTriggerEnter(Collider other)
    {
        // check if the colliding object has the "HittingObject" tag
        if (other.gameObject.CompareTag("HittingObject"))
        {
            // decrement the health
            health -= 10;

            // check if health is depleted
            if (health <= 0)
            {
                // destroy the gameobject
                Destroy(gameObject);
            }
        }
    }
}
