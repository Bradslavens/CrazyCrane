using UnityEngine;
using System.Collections;

public class NPCControllerc : MonoBehaviour
{
    public Transform target; // the target to walk towards
    public float moveSpeed = 3.0f; // the speed at which to move
    public float rotationSpeed = 3.0f; // the speed at which to rotate
    public float jumpForce = 5.0f; // the force to apply when jumping
    public float climbSpeed = 2.0f; // the speed at which to climb

    private Rigidbody rb; // the Rigidbody component
    private bool isGrounded; // whether the NPC is grounded or not

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = false;
    }

    void FixedUpdate()
    {
        // check if the NPC is on the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        // if not, apply gravity
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * 9.8f, ForceMode.Acceleration);
        }
        else
        {
            // move towards the target
            Vector3 targetDir = (target.position - transform.position).normalized;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            Debug.DrawLine(transform.position, target.position, Color.green);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // if the NPC collides with an obstacle, climb it
        if (other.CompareTag("Obstacle"))
        {
            StartCoroutine(ClimbObstacle(other));
        }
    }

    IEnumerator ClimbObstacle(Collider obstacle)
    {
        // disable gravity
        rb.useGravity = false;

        // calculate the climb direction
        Vector3 climbDir = obstacle.transform.up;
        Vector3 climbStart = transform.position;

        // move towards the obstacle
        while (Vector3.Distance(transform.position, obstacle.transform.position) > 0.5f)
        {
            Vector3 targetDir = (obstacle.transform.position - transform.position).normalized;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            yield return null;
        }

        // climb up the obstacle
        while (Vector3.Distance(transform.position, climbStart + climbDir) > 0.5f)
        {
            transform.position += climbDir * climbSpeed * Time.deltaTime;
            yield return null;
        }

        // jump down the other side of the obstacle
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        yield return new WaitForSeconds(0.5f);
        rb.AddForce(Vector3.down * jumpForce, ForceMode.VelocityChange);

        // re-enable gravity
        rb.useGravity = true;
    }
}
