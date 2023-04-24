using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;
    public float climbSpeed = 2f;
    public float detectionDistance = 5f;
    public LayerMask obstacleLayer;

    private Rigidbody rb;
    private bool isClimbing = false;
    private Vector3 climbDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isClimbing)
        {
            // Rotate the enemy to face the target
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);

            MoveTowardsTarget();
            DetectObstacle();
        }
        else
        {
            Climb();
        }
    }


    void MoveTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0;

        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }



    void DetectObstacle()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up;
        Vector3 direction = transform.forward;

        // Draw a debug line to visualize the raycast
        Debug.DrawRay(origin, direction * detectionDistance, Color.red, 1.0f);

        if (Physics.SphereCast(origin, 0.5f, direction, out hit, detectionDistance, obstacleLayer))
        {
            Debug.Log("Obstacle detected");
            isClimbing = true;
        }
        else
        {
            Debug.Log("No obstacle detected");
        }
    }



    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            isClimbing = true;
            rb.isKinematic = true;
            Debug.Log("Entered collision with obstacle");
        }
    }

    void Climb()
    {
        if (isClimbing)
        {
            transform.position += (Vector3.up + climbDirection) * climbSpeed * Time.deltaTime;
            Debug.Log("Climbing");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            isClimbing = false;
            rb.isKinematic = false;
            Debug.Log("Exited collision with obstacle");
        }
    }
}
