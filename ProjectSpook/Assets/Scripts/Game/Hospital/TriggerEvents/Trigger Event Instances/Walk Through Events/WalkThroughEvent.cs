using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkThroughEvent : TriggerEvent
{
    public override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.GetComponent<Collider>().enabled = false;
        }
       
    }
}
