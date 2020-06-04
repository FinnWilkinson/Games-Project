using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollPhysics : FollowPhysics
{
    private float startingY;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        startingY = this.transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowTarget();
    }

    protected override void FollowTarget()
    {
        Vector3 newPosition = new Vector3(target.transform.position.x, startingY, target.transform.position.z);
        rb.MovePosition(newPosition);
    }

}
