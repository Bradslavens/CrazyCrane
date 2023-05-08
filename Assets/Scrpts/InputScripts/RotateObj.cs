using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObj : MonoBehaviour
{
    //private float moveSpeed = 5.0f;
    private float rotateSpeed = 100.0f;
    private InputAction moveLRAction;
    private InputAction rotateAction;

    private void Start()
    {
        // Get the moveLR and rotate actions from the PlayerInput component
        //moveLRAction = GetComponent<PlayerInput>().actions.FindAction("MoveLR");
        rotateAction = GetComponent<PlayerInput>().actions.FindAction("Rotate");
    }

    private void FixedUpdate()
    {
        // Read the input value of the moveLR and rotate actions
        //float moveLRInputValue = moveLRAction.ReadValue<float>();
        float rotateInputValue = rotateAction.ReadValue<float>();

        // Move the player based on the moveLR input value
        //transform.position += new Vector3(moveLRInputValue, 0, 0) * moveSpeed * Time.deltaTime;

        // Rotate the player based on the rotate input value
        transform.Rotate(Vector3.up, rotateInputValue * rotateSpeed * Time.deltaTime);
    }
}
