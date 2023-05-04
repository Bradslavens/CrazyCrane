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
    private bool isClimbing; // whether the NPC is currently climbing or not

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = false;
        isClimbing = false;
    }

    void FixedUpdate()
    {
        if (isClimbing)
        {
            // if climbing, don't move forward
            return;
        }

        // check if the NPC is on the ground
        Vector3 raycastOrigin = transform.position - new Vector3(0.0f, 0.5f, 0.0f);
        float raycastDistance = 0.5f + 0.1f;
        RaycastHit hitInfo;
        isGrounded = Physics.Raycast(raycastOrigin, Vector3.down, out hitInfo, raycastDistance);

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

    void OnCollisionEnter(Collision collision)
    {
        // if the NPC collides with an obstacle, climb it
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(ClimbObstacle(collision));
        }
    }

    IEnumerator ClimbObstacle(Collision obstacle)
    {
        // disable gravity and set climbing flag
        rb.useGravity = false;
        isClimbing = true;

        // calculate the climb direction and distance
        Vector3 climbDir = obstacle.contacts[0].normal;
        Vector3 climbStart = transform.position;
        float climbDistance = obstacle.collider.bounds.size.y - 1.0f; // subtract 1.0f to account for NPC height

        // move towards the obstacle
        while (Vector3.Distance(transform.position, obstacle.contacts[0].point) > 0.5f)
        {
            Vector3 targetDir = (obstacle.contacts[0].point - transform.position).normalized;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            yield return null;
        }

        // climb up the obstacle
        while (Vector3.Distance(transform.position, climbStart + climbDir * climbDistance) > 0.5f)
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
