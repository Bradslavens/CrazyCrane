using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 5f;
    public float movementSpeed = 5f;
    private Animator animator;
    private bool isMoving = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0f; // Ignore any height difference
            direction.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

            animator.Play("Running");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            isMoving = false;
            animator.Play("Shooting");
        }
    }
}
