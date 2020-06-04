using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Holdable : Grabbable
{
    protected Quaternion heldRotation;
    //Store the held rotation for each holdable

    public override void Interact(Hand hand)
    {
        
        //If the handsocket isn't currently holding an object
        if (!hand.GetSocket().GetStoredObject())
        {
            //Rotate correctly
            activeRotation = heldRotation;
            //Set the hand as the parent of the object. If the hand isn't a parent, then we get weird repositioning of the object
            this.transform.parent = hand.transform;
            //Call base interaction of a grabbable (attaching to a hand socket)
            hand.SetPickedUp(true);
            base.Interact(hand);
        }
         
    }

    //Different instances of holdables have different actions. So we use a virtual function
    public virtual void PerformAction(Hand hand)
    {

    }

    public override void EndInteraction(Hand hand)
    {
        //When we drop the moveable we want to release it from the out socket and calculate the velocity if thrown
        this.transform.parent = null;
        base.EndInteraction(hand);
        attachedHand = null;
        hand.SetPickedUp(false);
    }

}
