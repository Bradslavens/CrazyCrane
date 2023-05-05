using UnityEngine;

public class HookManager : MonoBehaviour
{
    public GameObject hookPrefab; // The prefab for the Crane Hook object

    private GameObject currentHook; // The current Crane Hook object

    private void Start()
    {
        // Instantiate the Crane Hook object at the same position as the Hook Manager objectcurrentHook = Instantiate(hookPrefab, transform.position, Quaternion.identity, transform);
        currentHook = Instantiate(hookPrefab, transform.position, Quaternion.identity, transform);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Destroy the current Crane Hook object and instantiate a new one at the same position as the Hook Manager object
            Destroy(currentHook);
            currentHook = Instantiate(hookPrefab, transform.position, Quaternion.identity, transform);

        }
    }
}
