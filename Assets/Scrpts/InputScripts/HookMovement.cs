using UnityEngine;
using UnityEngine.InputSystem;

public class HookMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 200.0f;
    private InputAction hookAction;
    private Transform hookTransform;

    private void Start()
    {
        hookAction = GetComponent<PlayerInput>().actions.FindAction("Hook");
        hookTransform = transform.Find("arm/Hook");

        if (hookAction == null)
        {
            Debug.LogError("Hook action not found!");
        }

        if (hookTransform == null)
        {
            Debug.LogError("Hook transform not found!");
        }
    }

    private void FixedUpdate()
    {
        float hookInputValue = hookAction.ReadValue<float>();
        Debug.Log("Hook input value: " + hookInputValue);

        if (hookTransform != null)
        {
            // Move the Hook child object based on the input value
            hookTransform.position += hookTransform.up * hookInputValue * moveSpeed * Time.deltaTime;
        }
        else
        {
            Debug.LogError("Hook transform not found!");
        }
    }
}
