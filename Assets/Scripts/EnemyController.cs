using UnityEngine;
using System.Collections;


public class EnemyController : MonoBehaviour
{
    public string targetTag = "Target";
    public string obstacleTag = "Obstacle";
    public float moveSpeed = 5f;
    public float climbSpeed = 2f;

    private Transform target;
    private bool isClimbing = false;

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
        if (target != null && !isClimbing)
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            isClimbing = true;
            StartCoroutine(ClimbObstacle(collision.gameObject));
        }
    }

    IEnumerator ClimbObstacle(GameObject obstacle)
    {
        float obstacleTopY = obstacle.transform.position.y + obstacle.transform.localScale.y / 2;
        while (transform.position.y < obstacleTopY)
        {
            transform.position += Vector3.up * climbSpeed * Time.deltaTime;
            yield return null;
        }

        isClimbing = false;
    }
}
