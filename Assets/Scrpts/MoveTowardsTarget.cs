using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    public Transform target;
    public float forwardRayDistance = 5f;
    public float downRayDistance = 5f;

    public float walkingSpeed = 5f;
    public float climbingSpeed = 2f;
    public float fallingSpeed = 10f;

    public enum NPCState
    {
        Climbing,
        Walking,
        Falling
    }

    private NPCState npcState;

    private void Update()
    {
        TurnNPCTowardsTarget(target);
        MoveNPCTowardsTarget(target);
        SetNPCState(forwardRayDistance, downRayDistance);
    }

    private bool ShootRayForward(float rayDistance)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
        {
            // Hit something
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
            return true;
        }
        else
        {
            // Did not hit anything
            Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
            return false;
        }
    }

    private bool ShootRayDown(float rayDistance)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, rayDistance))
        {
            // Hit something
            Debug.DrawRay(transform.position, -Vector3.up * hit.distance, Color.green);
            return true;
        }
        else
        {
            // Did not hit anything
            Debug.DrawRay(transform.position, -Vector3.up * rayDistance, Color.red);
            return false;
        }
    }

    public void SetNPCState(float forwardRayDistance, float downRayDistance)
    {
        if (ShootRayForward(forwardRayDistance))
        {
            // Ray hit something in front of the NPC, so it's climbing
            npcState = NPCState.Climbing;
        }
        else if (ShootRayDown(downRayDistance))
        {
            // Ray hit something beneath the NPC, so it's walking
            npcState = NPCState.Walking;
        }
        else
        {
            // Ray did not hit anything, so it's falling
           // npcState = NPCState.Falling;
        }
    }

    private void MoveNPCTowardsTarget(Transform target)
    {
        if (npcState == NPCState.Walking)
        {
            // Move the NPC towards the target
            if (target != null)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * walkingSpeed * Time.deltaTime;
            }
        }
        else if (npcState == NPCState.Climbing)
        {
            // Move the NPC up the y-axis
            transform.position += Vector3.up * climbingSpeed * Time.deltaTime;
        }
        else if (npcState == NPCState.Falling)
        {
            // Move the NPC down the y-axis
            transform.position += Vector3.down * fallingSpeed * Time.deltaTime;
        }
    }

    public void TurnNPCTowardsTarget(Transform target)
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }




}
