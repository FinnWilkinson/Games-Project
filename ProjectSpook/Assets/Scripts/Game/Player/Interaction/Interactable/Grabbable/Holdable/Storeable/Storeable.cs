using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storeable : Holdable
{
    protected Quaternion storedRotation;
    protected bool isStored;
    public Socket beltSocket;

    public override void Interact(Hand hand)
    {
        base.Interact(hand);
        if (hand.GetSocket().GetStoredObject() == this) {
            isStored = false;
            this.GetComponent<Collider>().isTrigger = false;
        } 
        
    }

    public override void EndInteraction(Hand hand)
    {
        base.EndInteraction(hand);
        isStored = true;
        AttachToBelt();
    }

    public void AttachToBelt()
    {
        activeRotation = storedRotation;
        AttachNewSocket(beltSocket);
        this.GetComponent<Collider>().isTrigger = true;
    }

    public Quaternion GetStoredRotation()
    {
        return storedRotation;
    }

    public void SetStored(bool stored)
    {
        isStored = stored;
    }

    public bool IsStored()
    {
        return isStored;
    }

    public bool IsSocketed()
    {
        return (this.InHand() || this.isStored);
    }

}
