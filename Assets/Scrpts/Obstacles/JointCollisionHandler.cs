using UnityEngine;

public class JointCollisionHandler : MonoBehaviour
{
    CharacterJoint characterJoint;

    private void Start()
    {
        characterJoint = GetComponent<CharacterJoint>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherBody = collision.rigidbody;

        if (otherBody != null)
        {
            Vector3 contactPoint = collision.contacts[0].point;

            // Set the connected body and anchor point of the character joint
            characterJoint.connectedBody = otherBody;
            characterJoint.anchor = characterJoint.transform.InverseTransformPoint(contactPoint);
        }
    }
}
