using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSlamEvent : WalkThroughEvent
{
    public List<Door> doors;

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Door d in doors)
            {
                d.SetLocked(true);
            }
            base.OnTriggerEnter(other);
        }
    }

}
