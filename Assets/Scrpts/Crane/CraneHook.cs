using UnityEngine;

public class CraneHook : MonoBehaviour
{
    public float speed = 5.0f; // The speed at which the hook moves down
    public float sphereCastRadius = 0.5f; // The radius of the sphere used for casting

    private bool isLowering = false; // Flag for whether the hook is currently lowering
    private bool isRaising = false; // Flag for whether the hook is currently raising
    private Vector3 originalPosition; // The original position of the Crane Hook object

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!isLowering && !isRaising)
            {
                isLowering = true;
                originalPosition = transform.position;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isRaising && !isLowering)
            {
                isRaising = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isLowering)
        {
            transform.Translate(Vector3.down * speed * Time.fixedDeltaTime);
        }
        else if (isRaising && Input.GetKey(KeyCode.R))
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, speed * Time.fixedDeltaTime);
            if (transform.position == originalPosition)
            {
                isRaising = false;
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, sphereCastRadius, Vector3.down, speed * Time.fixedDeltaTime);
            Debug.Log("Trigger collision detected with " + other.name + ". Number of contact points: " + hits.Length);
            foreach (RaycastHit hit in hits)
            {
                Debug.Log("Contact point: " + hit.point);
                CharacterJoint joint = hit.collider.GetComponent<CharacterJoint>();
                if (joint != null)
                {
                    isLowering = false;
                    joint.connectedBody = GetComponent<Rigidbody>();
                    joint.anchor = joint.transform.InverseTransformPoint(hit.point);
                    isRaising = true;
                }
            }
        }
    }
}
