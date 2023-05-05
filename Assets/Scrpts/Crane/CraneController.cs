using UnityEngine;

public class CraneController : MonoBehaviour
{
    public float rotationSpeed = 50.0f;

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, horizontalInput * rotationSpeed * Time.fixedDeltaTime, 0);
    }
}
