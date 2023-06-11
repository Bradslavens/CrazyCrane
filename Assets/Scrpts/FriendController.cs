using UnityEngine;

public class FriendController : MonoBehaviour
{
    public GameObject target;
    public float speed = 3.0f;
    public float shootingRange = 1f;

    private Animator animator;

    public enum CharacterState
    {
        Running,
        Shooting
    }

    public CharacterState state = CharacterState.Running;


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

            // Look at the target (y-axis only)
            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

            // Check the distance to the target
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= shootingRange)
            {
                state = CharacterState.Shooting;
            }

            // Check the CharacterState of the FriendController component
            if (state == CharacterState.Shooting)
            {
                // Play the Shooting animation
                animator.SetTrigger("Shooting");
            }

            if (state == CharacterState.Running)
            {
                // Move the character towards the target
                transform.position = transform.position + direction * speed * Time.deltaTime;
            }
        }
    }
}
