using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float fallSpeed = 5f; // Speed at which the object falls
    public float walkSpeed = 3f; // Speed at which the object walks
    public float climbSpeed = 5f; // Speed at which the object climbs
    public float raycastRange = 10f; // Range of the raycast
    public Transform target; // Target position for walking state

    private Transform rayGun; // Reference to the RayGun child object
    private RaycastHit raycastHit; // Store the raycast hit information
    private Vector3 hitPoint; // Store the hit point
    private NPCState state; // Current state of the NPC
    private Animator animator; // Animator component reference
    private EnemyManager enemyManager; // Reference to the EnemyManager script


    private enum NPCState
    {
        Falling,
        Walking,
        Climbing,
        Idle
    }

    private void Awake()
    {
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
    }

    private void Start()
    {
        rayGun = transform.Find("RayGun"); // Assuming the child object is named "RayGun"
        state = NPCState.Falling; // Set the initial state to Falling

        // Get the Animator component on the child object
        animator = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        // Perform the raycast downwards

        if (Physics.Raycast(rayGun.position, Vector3.down, out raycastHit, raycastRange))
        {
            hitPoint = raycastHit.point; // Store the hit point

            // Draw the raycast
            Debug.DrawRay(rayGun.position, Vector3.down * raycastRange, Color.red);

            // Check the difference between transform.position.y and hitPoint.y
            float difference = transform.position.y - hitPoint.y;

            if (difference < 0f)
            {
                state = NPCState.Climbing;
            }
            else
            {
                state = NPCState.Walking;
            }
        }
        else
        {
            state = NPCState.Falling;
        }

        // Update the object's behavior based on the state
        switch (state)
        {

            case NPCState.Idle:
                Vector3 noMove = new Vector3(0, 0, 0);
                // Translate down at fall speed
                transform.Translate(noMove);
                animator.Play("idle"); // Play the idle animation
                break;

            case NPCState.Falling:
                // Translate down at fall speed
                transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
                animator.Play("idle"); // Play the idle animation
                break;

            case NPCState.Walking:
                if (target != null)
                {
                    // Calculate the direction to the target on the xz-plane
                    Vector3 targetDirection = target.position - transform.position;
                    targetDirection.y = 0f; // Set the y-component to 0 to ignore vertical rotation

                    if (targetDirection != Vector3.zero)
                    {
                        // Rotate towards the target position on the y-axis only
                        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);
                    }
                }

                // Translate forward at walk speed
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
                animator.Play("walking"); // Play the walking animation
                break;


            case NPCState.Climbing:
                // Translate up at climb speed
                Debug.Log("CCC");
                transform.Translate(Vector3.up * climbSpeed * Time.deltaTime);
                animator.Play("climbing"); // Play the climbing animation
                break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.transform == target)
        {
            state = NPCState.Idle;
            animator.Play("idle");
        }
    }

}
