using UnityEngine;

public class FriendController : MonoBehaviour
{
    public GameObject target;
    public float speed = 3.0f;

    public enum CharacterState
    {
        Running,
        Shooting
    }

    public CharacterState state;

    void Update()
    {
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // Make sure the character moves along the ground plane by setting y to zero
            direction.y = 0;

            // Look at the target (y-axis only)
            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

            if (state == CharacterState.Running)
            {
                // Move the character towards the target
                transform.position = transform.position + direction * speed * Time.deltaTime;
            }
        }
    }
}
