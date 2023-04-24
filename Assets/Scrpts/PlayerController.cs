using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the player
    public float jumpForce = 10f; // Jump force of the player
    private Rigidbody rb; // Rigidbody of the player
    private bool isGrounded; // Boolean to check if the player is grounded
    public Transform groundCheck; // Transform of the ground check object
    public float groundCheckRadius = 0.1f; // Radius of the ground check sphere
    public LayerMask whatIsGround; // Layer mask for the ground

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);

        // Move the player forward, backward, left or right
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rb.velocity = movement.normalized * moveSpeed;

        // Rotate the player towards the direction of movement
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }

    void Update()
    {
        // Make the player jump if they are grounded and the jump button is pressed
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
