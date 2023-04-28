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
            Vector3 obstacleNormal = hit.normal;
            Vector3 climbDirection = Vector3.Cross(Vector3.up, obstacleNormal).normalized;

            npc.position += climbDirection * climbSpeed * Time.deltaTime;

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
        npc.position += Vector3.down * climbSpeed * Time.deltaTime;

        if (npc.position.y <= 0.0f)
        {
            isFalling = false;
        }
    }
}
