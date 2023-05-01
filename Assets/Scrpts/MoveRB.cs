using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRB : MonoBehaviour
{
    public float speed = 10.0f;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(0, 0, 1) * speed * Time.deltaTime;
        rb.MovePosition(transform.position + move);
    }
}
