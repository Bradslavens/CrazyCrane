using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed = 5.0f;
    public float stoppingDistance = 1.0f;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        if (target == null)
        {
            target = GameObject.Find("Target");
        }

        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("No NavMeshAgent component found on the enemy GameObject.");
        }
        else
        {
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.stoppingDistance = stoppingDistance;
        }
    }

    void Update()
    {
        if (target != null && navMeshAgent != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToTarget > navMeshAgent.stoppingDistance)
            {
                navMeshAgent.SetDestination(target.transform.position);
            }
            else
            {
                navMeshAgent.ResetPath();
            }
        }
    }
}
