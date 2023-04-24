using UnityEngine;
using UnityEngine.AI;

public class Climber : MonoBehaviour
{
    public float sphereCastRadius = 0.5f;
    public float maxDistance = 3.0f;
    public float climbSpeed = 2.0f;
    public float climbExtraDistance = 1.0f;
    public LayerMask detectionLayers;

    private RaycastHit hitInfo;
    private bool climbing = false;
    private float climbedExtraDistance = 0.0f;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        if (navMeshAgent == null)
        {
            Debug.LogError("No NavMeshAgent component found on the enemy GameObject.");
        }

        if (rb == null)
        {
            Debug.LogError("No Rigidbody component found on the enemy GameObject.");
        }
    }

    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        float distance = maxDistance;

        if (!climbing && Physics.SphereCast(origin, sphereCastRadius, direction, out hitInfo, distance, detectionLayers))
        {
            Debug.Log("Object detected in front: " + hitInfo.collider.name);
            climbing = true;
            climbedExtraDistance = 0.0f;
            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = false;
            }
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }

        if (climbing)
        {
            float climbDelta = climbSpeed * Time.deltaTime;
            transform.position += Vector3.up * climbDelta;
            climbedExtraDistance += climbDelta;

            if (!Physics.SphereCast(origin, sphereCastRadius, direction, out hitInfo, distance, detectionLayers))
            {
                if (climbedExtraDistance >= climbExtraDistance)
                {
                    climbing = false;
                    if (navMeshAgent != null)
                    {
                        navMeshAgent.enabled = true;
                    }
                    if (rb != null)
                    {
                        rb.isKinematic = false;
                    }
                }
            }
            else
            {
                climbedExtraDistance = 0.0f;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.forward * maxDistance, sphereCastRadius);
    }
}
