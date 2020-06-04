using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPhysics : MonoBehaviour
{
    public Transform target;
    protected Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowTarget();
    }

    protected virtual void FollowTarget()
    {
        rb.MovePosition(target.transform.position);
    }
}
