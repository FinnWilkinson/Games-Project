using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : WalkThroughEvent
{
    public List<Rigidbody> rbs;
    public float xSpeed, ySpeed, zSpeed;
    // Start is called before the first frame update

    private void Start()
    {
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("TRIGGER ENTER");
            ThrowBody();
        }
    }



    public void ThrowBody()
    {
        Vector3 speed = new Vector3(xSpeed, ySpeed, zSpeed);
        Debug.Log("THROWING");
        foreach (Rigidbody rb in rbs)
        {
            rb.velocity = speed;
        }
    }
}
