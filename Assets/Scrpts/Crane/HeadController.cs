using UnityEngine;
using TMPro;

public class HeadController : MonoBehaviour
{
    public Transform cranehead;
    public TMP_Text crosshairs;
    public float moveSpeed = 5.0f;
    public LayerMask obstacleLayer;

    private bool isMovingHead = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            isMovingHead = !isMovingHead;
        }

        if (isMovingHead)
        {
            RaycastHit hit;
            Vector3 rayOrigin = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            Debug.DrawRay(rayOrigin, Camera.main.transform.forward * 100f, Color.green);

            if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit, Mathf.Infinity, obstacleLayer))
            {
                Vector3 hitPoint = hit.point;
                hitPoint.y = cranehead.position.y; // keep the y position fixed
                cranehead.position = Vector3.MoveTowards(cranehead.position, hitPoint, moveSpeed * Time.deltaTime);
            }
            else
            {
                Debug.Log("No object hit or object does not belong to the obstacle layer");
            }
        }

        crosshairs.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
    }
}
