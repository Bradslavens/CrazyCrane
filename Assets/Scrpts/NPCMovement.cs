using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform npc;
    public Transform target;
    public float moveSpeed = 3.0f;
    public float climbSpeed = 2.0f;
    public float obstacleThreshold = 1.0f;
    public LayerMask obstacleLayerMask;

    private Vector3 targetDirection;
    private bool isClimbing = false;
    private bool isFalling = false;

    void Update()
    {
        if (!isClimbing && !isFalling)
        {
            MoveTowardsTarget();
        }
        else if (isClimbing)
        {
            ClimbOverObstacle();
        }
        else if (isFalling)
        {
            FallFromObstacle();
        }
    }

    void MoveTowardsTarget()
    {
        targetDirection = (target.position - npc.position).normalized;

        if (!IsObstacleInTheWay())
        {
            npc.position += targetDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            isClimbing = true;
        }
    }

    bool IsObstacleInTheWay()
    {
        RaycastHit hit;
        if (Physics.Raycast(npc.position, targetDirection, out hit, obstacleThreshold, obstacleLayerMask))
        {
            return hit.collider != null;
        }
        return false;
    }

    void ClimbOverObstacle()
    {
        RaycastHit hit;
        if (Physics.Raycast(npc.position, targetDirection, out hit, obstacleThreshold, obstacleLayerMask))
        {
            npc.position += Vector3.up * climbSpeed * Time.deltaTime;

            if (npc.position.y > hit.point.y + 1.0f)
            {
                isClimbing = false;
            }
            else if (npc.position.y < hit.point.y)
            {
                isFalling = true;
                isClimbing = false;
            }
        }
        else
        {
            isClimbing = false;
        }
    }



    void FallFromObstacle()
    {
        Vector3 fallTarget = new Vector3(npc.position.x, 0.0f, npc.position.z); // Target position at NPC's current position
        targetDirection = (fallTarget - npc.position).normalized;

        npc.position += targetDirection * moveSpeed * Time.deltaTime;

        if (npc.position.y <= 0.0f)
        {
            isFalling = false;
        }
    }

}
