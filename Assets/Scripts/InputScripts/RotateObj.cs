using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObj : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 100.0f;
    private InputAction rotateAction;
    private Transform armTransform;

    private void Start()
    {
        rotateAction = GetComponent<PlayerInput>().actions.FindAction("Rotate");
        armTransform = transform.Find("arm");
    }

    private void FixedUpdate()
    {
        float rotateInputValue = rotateAction.ReadValue<float>();
        armTransform.Rotate(Vector3.up, rotateInputValue * rotateSpeed * Time.deltaTime);
    }
}
