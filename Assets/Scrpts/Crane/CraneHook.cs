using UnityEngine;

public class CraneHook : MonoBehaviour
{
    public float speed = 5.0f; // The speed at which the hook moves down
    public float sphereCastRadius = 0.5f; // The radius of the sphere used for casting

    private bool isLowering = false; // Flag for whether the hook is currently lowering

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            isLowering = true;
        }
    }

    private void FixedUpdate()
    {
        if (isLowering)
        {
            transform.Translate(Vector3.down * speed * Time.fixedDeltaTime);
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
            }
        }
    }
}
