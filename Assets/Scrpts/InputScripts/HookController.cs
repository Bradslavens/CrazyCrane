using UnityEngine;
using UnityEngine.InputSystem;

public class HookController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 200.0f;
    private InputAction hookAction;

    private void Start()
    {
        // Get the Hook action from the PlayerInput component
        hookAction = GetComponent<PlayerInput>().actions.FindAction("Hook");
    }

    private void FixedUpdate()
    {
        // Read the input value of the Hook action
        float hookInputValue = hookAction.ReadValue<float>();
        Debug.Log(hookInputValue);

        // Move the player based on the input value
        transform.position += transform.forward * hookInputValue * moveSpeed * Time.deltaTime;
    }
}
