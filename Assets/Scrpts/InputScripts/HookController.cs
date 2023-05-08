using UnityEngine;
using UnityEngine.InputSystem;

public class HookController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 200.0f;
    private InputAction hookAction;
    private Transform hookTransform;

    private void Start()
    {
        hookAction = GetComponent<PlayerInput>().actions.FindAction("Head");
        hookTransform = transform.Find("arm/Hook");
    }

    private void FixedUpdate()
    {
        float hookInputValue = hookAction.ReadValue<float>();
        Debug.Log(hookInputValue);

        // Move the Hook child object based on the input value
        hookTransform.position += hookTransform.forward * hookInputValue * moveSpeed * Time.deltaTime;
    }
}
