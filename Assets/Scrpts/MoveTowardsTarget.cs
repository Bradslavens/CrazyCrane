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

    private void SetNPCState(float forwardRayDistance, float downRayDistance)
    {
        if (npcState == NPCState.Walking)
        {
            if (!ShootRaycastDown(downRayDistance, downRaycastObject))
            {
                npcState = NPCState.Falling;
                Debug.Log("falling");
            }
            else if (ShootRaycastForward(forwardRayDistance, forwardRaycastObject))
            {
                npcState = NPCState.Climbing;
                Debug.Log("climbing");
            }
        }
        else if (npcState == NPCState.Falling)
        {
            if (ShootRaycastDown(downRayDistance, downRaycastObject))
            {
                npcState = NPCState.Walking;

                Debug.Log("walking");
            }
        }
        else if (npcState == NPCState.Climbing)
        {
            if (!ShootRaycastForward(forwardRayDistance, forwardRaycastObject))
            {
                npcState = NPCState.Walking;

                Debug.Log("walking");
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


