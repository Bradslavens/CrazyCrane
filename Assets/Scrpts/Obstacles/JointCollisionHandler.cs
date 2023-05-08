using UnityEngine;

public class JointCollisionHandler : MonoBehaviour
{
    CharacterJoint characterJoint;
    DisconnectCharacterJoint disconnectJoint;

    private void Start()
    {
        characterJoint = GetComponent<CharacterJoint>();
        disconnectJoint = GetComponent<DisconnectCharacterJoint>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hook"))
        {
            Rigidbody otherBody = collision.rigidbody;

            if (otherBody != null)
            {
                Vector3 contactPoint = collision.contacts[0].point;

                // Set the connected body and anchor point of the character joint
                characterJoint.connectedBody = otherBody;
                characterJoint.anchor = characterJoint.transform.InverseTransformPoint(contactPoint);

                // Update the hooked boolean on DisconnectCharacterJoint component
                disconnectJoint.hooked = true;
            }
        }
    }
}
