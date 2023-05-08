using UnityEngine;
using UnityEngine.InputSystem;

public class DestroyAndRespawnHook : MonoBehaviour
{
    // Set this in the Inspector to the prefab of the hook you want to instantiate
    public GameObject hookPrefab;
    public Transform hookParent;
    private InputAction obstacleAction;

    private GameObject currentHook;

    private void Start()
    {
        currentHook = GameObject.Find("Hook");
    }

    private void Update()
    {
        obstacleAction = GetComponent<PlayerInput>().actions.FindAction("Obstacle");
        // Check if the input value is equal to 1
        float valueObstacleAction = obstacleAction.ReadValue<float>();

        if (valueObstacleAction == 1.0f && currentHook != null)
        {
            // Get the position and rotation of the current hook object
            Vector3 position = currentHook.transform.position;
            Quaternion rotation = currentHook.transform.rotation;

            // Destroy the current hook object
            Destroy(currentHook);

            // Instantiate a new hook object at the same position and rotation,
            // and make it a child of the specified parent transform
            GameObject newHook = Instantiate(hookPrefab, position, rotation, hookParent);

            // Set the new hook object as the current hook
            currentHook = newHook;
        }
    }
}
