using UnityEngine;
using TMPro;

public class RaycastDown : MonoBehaviour
{
    // Add a public variable for the TextMeshPro object
    public TextMeshPro textMeshProObject;

    void Update()
    {
        RaycastHit hit;

        // Perform a raycast down from the Ray child object's position with an unlimited distance
        Transform rayTransform = transform.Find("Ray");
        if (Physics.Raycast(rayTransform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            // If the raycast hits something, print the hit object's name and the distance
            float distance = hit.distance;
            Debug.Log("Hit object: " + hit.collider.gameObject.name + ", Distance: " + distance);

            // Set the position of the TextMeshPro object to the hit point with an added Y-offset of 0.5
            textMeshProObject.transform.position = new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z);
        }
        else
        {
            Debug.Log("No object hit");
        }

        // Draw a debug ray to visualize the raycast
        Debug.DrawRay(rayTransform.position, Vector3.down * 1000f, Color.red);
    }
}
