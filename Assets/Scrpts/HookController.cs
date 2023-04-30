using UnityEngine;
using TMPro;

public class HookController : MonoBehaviour
{
    public Transform cranehook;
    public TMP_Text crosshairs;
    public float moveSpeed = 5.0f;

    private bool isMovingHook = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            isMovingHook = !isMovingHook;
        }

        if (isMovingHook)
        {
            RaycastHit hit;
            Vector3 rayOrigin = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            Debug.DrawRay(rayOrigin, Camera.main.transform.forward * 100f, Color.green);

            if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit))
            {
                Vector3 hitPoint = hit.point;
                hitPoint.y = cranehook.position.y; // keep the y position fixed
                cranehook.position = Vector3.MoveTowards(cranehook.position, hitPoint, moveSpeed * Time.deltaTime);
            }
            else
            {
                Debug.Log("No object hit");
            }
        }

        crosshairs.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
    }
}
