using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    public GameObject target; // The target game object the NPC will move towards
    public float speed = 5f; // The speed at which the NPC will move towards the target
    public float stoppingDistance = 1f; // The distance at which the NPC will stop moving towards the target
    public float avoidDistance = 2f; // The distance at which the NPC will start avoiding obstacles
    public LayerMask obstacleLayer; // The layer(s) that represent obstacles

    private bool isMoving = true;

    private void Update()
    {
        // If the target is null or the NPC has already reached the target, exit the method
        if (target == null || !isMoving)
            return;

        // Calculate the direction from the NPC to the target
        Vector3 direction = (target.transform.position - transform.position).normalized;

        // Cast a ray in the direction of the target to check for obstacles
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, avoidDistance, obstacleLayer))
        {
            // If an obstacle is detected, steer the NPC away from the obstacle
            Vector3 avoidDirection = Vector3.Reflect(direction, hit.normal);
            transform.rotation = Quaternion.LookRotation(avoidDirection);
        }
        else
        {
            // If no obstacle is detected, move towards the target
            transform.rotation = Quaternion.LookRotation(direction);
            if (Vector3.Distance(transform.position, target.transform.position) > stoppingDistance)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            else
            {
                isMoving = false;
                Debug.Log("Reached target!");
            }
        }
    }
}
