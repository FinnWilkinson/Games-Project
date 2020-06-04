using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSlot : Interactable
{
    private Socket beltSocket = null;

    private void Awake()
    {
        beltSocket = GetComponent<Socket>();
    }

    public override void Interact(Hand hand)
    {
        //We want to get the hand socket and the object it's holding
        Socket handSocket = hand.GetSocket();
        Grabbable heldObject = handSocket.GetStoredObject(); 

        /*If we're holding an object and it is storable and then try to interact with a belt slot. Then try and store the item.
          If this seems verbose, check reasoning in Hand.TryAction()*/
        if (heldObject && heldObject.GetComponent<Storeable>())
        {
            TryStore(heldObject.gameObject.GetComponent<Storeable>(), hand);
        }
        /*If we're not holding an object then try retrieve.
          The held object could be a squeezable door handle, so we use an else if instead of just else.
          (Don't want to walk around with a door handle in the players pocket)*/
        else if (!heldObject)
        {
            //Otherwise try to retrieve, provided we're holding nothing and not a moveable
            TryRetrieve(hand);
        }
    }

    private void TryStore(Storeable newStoreable, Hand hand)
    {
        //See if socket already contains object, if not then attach the storeable
        if (!beltSocket.GetStoredObject())
        {
            newStoreable.SetStored(true);
            newStoreable.EndInteraction(hand);
            newStoreable.AttachToBelt();
            hand.SetPickedUp(false);
        }


    }

    private void TryRetrieve(Hand hand)
    {
        Grabbable objectToRetrieve = beltSocket.GetStoredObject();
        //See if socket already contains object. If it does, attach it to the hand
        if (objectToRetrieve)
            objectToRetrieve.Interact(hand);

    }
}
