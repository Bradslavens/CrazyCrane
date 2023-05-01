using UnityEngine;
using UnityEngine.AI;

public class MoveToDestination : MonoBehaviour
{
    public GameObject target;

    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null && target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }

    void Update()
    {
        if (target != null)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.SetDestination(target.transform.position);
            }
        }
    }
}
