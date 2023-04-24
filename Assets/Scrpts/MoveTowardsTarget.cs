using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    public GameObject target; // The target game object the NPC will move towards
    public float speed = 5f; // The speed at which the NPC will move towards the target
    public float stoppingDistance = 1f; // The distance at which the NPC will stop moving towards the target

    private bool isMoving = true;

    private void Update()
    {
        // If the target is null or the NPC has already reached the target, exit the method
        if (target == null || !isMoving)
            return;

        // Calculate the direction from the NPC to the target
        Vector3 direction = (target.transform.position - transform.position).normalized;

        // If the NPC is farther away from the target than the stopping distance, move towards the target
        if (Vector3.Distance(transform.position, target.transform.position) > stoppingDistance)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
        else // Otherwise, stop moving
        {
            isMoving = false;
            Debug.Log("Reached target!");
        }
    }
}
