using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public string targetTag = "Target"; // Set the target's tag in the Inspector
    public float moveSpeed = 5f;

    private Transform target;

    void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
    }

    void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
