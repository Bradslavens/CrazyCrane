using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 50.0f;
    public float minAngle = -45.0f;
    public float maxAngle = 45.0f;

    private float currentAngle = 0.0f;

    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        currentAngle -= verticalInput * rotationSpeed * Time.fixedDeltaTime;
        currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);

        transform.localRotation = Quaternion.Euler(currentAngle, 0, 0);
    }
}
