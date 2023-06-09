using UnityEngine;
using UnityEngine.InputSystem;

public class DisconnectCharacterJoint : MonoBehaviour
{
    private CharacterJoint characterJoint;
    private InputAction obstacleAction;
    public bool hooked = false;

    void Start()
    {
        characterJoint = GetComponent<CharacterJoint>();
        obstacleAction = GetComponent<PlayerInput>().actions.FindAction("Obstacle");
    }

    void FixedUpdate()
    {
        float inputValue = obstacleAction.ReadValue<float>();
        Debug.Log("obstacle input value " + inputValue);

        if (hooked && inputValue == 1.0f)
        {
            // Check if the CharacterJoint component exists
            if (characterJoint != null)
            {
                // Destroy the CharacterJoint component
                Destroy(characterJoint);
            }
        }
    }
}
