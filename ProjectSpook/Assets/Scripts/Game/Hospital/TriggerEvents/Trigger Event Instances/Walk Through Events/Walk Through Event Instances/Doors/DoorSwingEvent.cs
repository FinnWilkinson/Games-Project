using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwingEvent : WalkThroughEvent
{
    public List<Door> doors;
    public float swingSpeed;
    public bool swingX, swingZ;

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Door d in doors)
            {
                d.SetLocked(false);
                Rigidbody doorRB = d.GetComponent<Rigidbody>();
                if (swingX)
                {
                    doorRB.velocity = new Vector3(swingSpeed, 0.0f, 0.0f);
                }
                else if (swingZ) 
                {
                    doorRB.velocity = new Vector3(0.0f, 0.0f, swingSpeed);
                }
            }
            base.OnTriggerEnter(other);
        }
    }

}
