using UnityEngine;
using UnityEngine.InputSystem;

public class DisconnectCharacterJoint : MonoBehaviour
{
    private CharacterJoint characterJoint;
    private InputAction obstacleAction;

    void Start()
    {
        characterJoint = GetComponent<CharacterJoint>();
        obstacleAction = GetComponent<PlayerInput>().actions.FindAction("Obstacle");

    }

    void FixedUpdate()
    {
        float inputValue = obstacleAction.ReadValue<float>();
        Debug.Log("obstacle input value " + inputValue);
        if(inputValue == 1.0f)
        {
            // Get a reference to the CharacterJoint component
            CharacterJoint characterJoint = GetComponent<CharacterJoint>();

            // Check if the CharacterJoint component exists
            if (characterJoint != null)
            {
                // Destroy the CharacterJoint component
                Destroy(characterJoint);
            }
        }
    }
}
