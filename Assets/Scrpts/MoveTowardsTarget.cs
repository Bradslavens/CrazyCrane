using UnityEngine;


public class MoveTowardsTarget : MonoBehaviour
{
    public Transform target;
    public float forwardRayDistance = 1f;
    public float downRayDistance = 1f;
    public float walkingSpeed = 5f;
    public float climbingSpeed = 2f;
    public float fallingSpeed = 10f;

    private NPCState npcState = NPCState.Walking;

    public enum NPCState
    {
        Climbing,
        Walking,
        Falling
    }

    private void FixedUpdate()
    {
        // Update the state of the NPC
        SetNPCState(forwardRayDistance, downRayDistance);

        // Move the NPC based on its state
        MoveNPCTowardsTarget();

        // Turn the NPC towards the target
        TurnNPCTowardsTarget();
    }

    private bool ShootRaycastForward(float rayDistance)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
            return false;
        }
    }

    private bool ShootRaycastDown(float rayDistance)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, rayDistance))
        {
            Debug.DrawRay(transform.position, -Vector3.up * hit.distance, Color.green);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, -Vector3.up * rayDistance, Color.red);
            return false;
        }
    }

    private void SetNPCState(float forwardRayDistance, float downRayDistance)
    {
        if (npcState == NPCState.Walking)
        {
            if (!ShootRaycastDown(downRayDistance))
            {
                npcState = NPCState.Falling;
            }
            else if (ShootRaycastForward(forwardRayDistance))
            {
                npcState = NPCState.Climbing;
            }
        }
        else if (npcState == NPCState.Falling)
        {
            if (ShootRaycastDown(downRayDistance))
            {
                npcState = NPCState.Walking;
            }
        }
        else if (npcState == NPCState.Climbing)
        {
            if (!ShootRaycastForward(forwardRayDistance))
            {
                npcState = NPCState.Walking;
            }
        }

    }

    private void MoveNPCTowardsTarget()
    {
        if (npcState == NPCState.Walking)
        {
            if (target != null)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * walkingSpeed * Time.fixedDeltaTime;
            }
        }
        else if (npcState == NPCState.Climbing)
        {
            transform.position += Vector3.up * climbingSpeed * Time.fixedDeltaTime;
        }
        else if (npcState == NPCState.Falling)
        {
            transform.position += Vector3.down * fallingSpeed * Time.fixedDeltaTime;
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
