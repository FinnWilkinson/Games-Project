using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : Squeezable
{
    private Transform parent;
    protected Rigidbody objectBody;

    public void Start()
    {
        parent = this.transform.parent;
        objectBody = parent.GetComponent<Rigidbody>();
    }


    public override void Interact(Hand hand)
    {
        if (!hand.GetSocket().GetStoredObject())
        {
            parent.GetComponent<RollPhysics>().enabled = true;
            this.GetComponent<RollPhysics>().enabled = false;
            base.Interact(hand);
            attachedHand = hand;
        }
        
        
    }

    public override void EndInteraction(Hand hand)
    {
        base.EndInteraction(hand);
        RepositionLogical();
        attachedHand = null;
    }

    protected virtual void RepositionLogical()
    {
        objectBody.isKinematic = true;
        parent.GetComponent<Rigidbody>().isKinematic = true;

        parent.GetComponent<RollPhysics>().enabled = false;
        this.GetComponent<RollPhysics>().enabled = true;

        objectBody.isKinematic = false;
        parent.GetComponent<Rigidbody>().isKinematic = false;

    }

}
