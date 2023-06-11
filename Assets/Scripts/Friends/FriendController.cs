using UnityEngine;

public class FriendController : MonoBehaviour
{
    public GameObject target;
    public float speed = 3.0f;
    public float rotationSpeed = 1.0f; // rotation speed
    public float shootingRange = 1f;

    private Animator animator;

    public enum CharacterState
    {
        Running,
        Shooting
    }

    public CharacterState currentState = CharacterState.Running;

    private void Start()
    {
        // Get the Animator component attached to the NPC
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // Make sure the character moves along the ground plane by setting y to zero
            direction.y = 0;

            // Check the distance to the target
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= shootingRange)
            {
                currentState = CharacterState.Shooting;
            }
            else
            {
                currentState = CharacterState.Running;
            }

            if (currentState == CharacterState.Shooting)
            {
                // Set the Shooting boolean to true
                animator.SetBool("Shooting", true);
            }
            else if (currentState == CharacterState.Running)
            {
                // Set the Shooting boolean to false
                animator.SetBool("Shooting", false);
                // Move the character towards the target
                transform.position += direction * speed * Time.deltaTime;
                // Make the character face the target
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
