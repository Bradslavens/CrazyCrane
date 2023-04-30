using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject craneHead;
    public LayerMask obstacleLayer;
    public KeyCode fireKey = KeyCode.Space;
    public float craneHeadSpeed = 5f;

    private bool canFire = false;
    private Vector3 firePosition;

    void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out hit, Mathf.Infinity, obstacleLayer))
            {
                canFire = true;
                firePosition = hit.point;
            }
            else
            {
                canFire = false;
            }
        }

        if (canFire)
        {
            Vector3 targetPosition = new Vector3(firePosition.x, craneHead.transform.position.y, firePosition.z);
            craneHead.transform.position = Vector3.MoveTowards(craneHead.transform.position, targetPosition, craneHeadSpeed * Time.deltaTime);
        }
    }
}
