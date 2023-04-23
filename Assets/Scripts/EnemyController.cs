using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public string targetTag = "Target";

    private Transform target;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
            navMeshAgent.SetDestination(target.position);
        }
    }

    void Update()
    {
        // You can add any additional logic or checks here, if necessary
    }
}
