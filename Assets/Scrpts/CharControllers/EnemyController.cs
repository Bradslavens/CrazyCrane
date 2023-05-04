using UnityEngine;
using System.Collections;


public enum EnemyState
{
    Walking,
    Climbing
}

public class EnemyController : MonoBehaviour
{
    public GameObject target;
    public float speed = 5f;
    public EnemyState enemyState = EnemyState.Walking;

    private bool isColliding = false;

    private void FixedUpdate()
    {
        if (enemyState == EnemyState.Walking)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.fixedDeltaTime);
        }
        else if (enemyState == EnemyState.Climbing)
        {
            transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.contacts[0].normal);

        if (collision.gameObject.CompareTag("Obstacle") && !isColliding)
        {
            isColliding = true;
            StartCoroutine(Climb());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && isColliding)
        {
            isColliding = false;
            StartCoroutine(Walk());
        }
    }

    private IEnumerator Climb()
    {
        yield return null;
        enemyState = EnemyState.Climbing;
    }

    private IEnumerator Walk()
    {
        yield return null;
        enemyState = EnemyState.Walking;
    }
}
