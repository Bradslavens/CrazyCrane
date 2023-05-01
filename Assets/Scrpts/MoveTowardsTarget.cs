using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    public Transform target;
    public Transform forwardRaycastObject;
    public float forwardRayDistance = 1f;
    public float walkingSpeed = 5f;
    public float climbingSpeed = 2f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // Enable gravity
    }

    private void FixedUpdate()
    {
        // Move the NPC based on its state
        MoveNPCTowardsTarget();

        // Turn the NPC towards the target
        TurnNPCTowardsTarget();
    }

    private bool ShootRaycastForward(float rayDistance, Transform raycastObject)
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastObject.position, raycastObject.forward, out hit, rayDistance))
        {
            Debug.DrawRay(raycastObject.position, raycastObject.forward * hit.distance, Color.green);
            return true;
        }
        else
        {
            Debug.DrawRay(raycastObject.position, raycastObject.forward * rayDistance, Color.red);
            return false;
        }
    }

    private void MoveNPCTowardsTarget()
    {
        if (target != null && ShootRaycastForward(forwardRayDistance, forwardRaycastObject))
        {
            rb.MovePosition(transform.position + Vector3.up * climbingSpeed * Time.deltaTime);
        }
        else if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * walkingSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void TurnNPCTowardsTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0f; // set the y component to zero to lock the rotation to the y-axis
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
