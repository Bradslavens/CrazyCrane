using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    public Transform target;
    public Transform forwardRaycastObject;
    public Transform downRaycastObject;
    public float forwardRayDistance = 1f;
    public float downRayDistance = 1f;
    public float walkingSpeed = 5f;
    public float climbingSpeed = 2f;

    private Rigidbody rigidbody;
    private bool isGrounded = true;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Check if NPC is on the ground
        CheckGrounded();

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

    private bool ShootRaycastDown(float rayDistance, Transform raycastObject)
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastObject.position, -raycastObject.up, out hit, rayDistance))
        {
            Debug.DrawRay(raycastObject.position, -raycastObject.up * hit.distance, Color.green);
            return true;
        }
        else
        {
            Debug.DrawRay(raycastObject.position, -raycastObject.up * rayDistance, Color.red);
            return false;
        }
    }

    private void CheckGrounded()
    {
        isGrounded = ShootRaycastDown(downRayDistance, downRaycastObject);
    }

    private void MoveNPCTowardsTarget()
    {
        if (isGrounded)
        {
            if (target != null && ShootRaycastForward(forwardRayDistance, forwardRaycastObject))
            {
                rigidbody.velocity = Vector3.up * climbingSpeed;
            }
            else
            {
                if (target != null)
                {
                    Vector3 direction = (target.position - transform.position).normalized;
                    direction.y = 0f;
                    rigidbody.velocity = direction * walkingSpeed;
                }
            }
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
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
