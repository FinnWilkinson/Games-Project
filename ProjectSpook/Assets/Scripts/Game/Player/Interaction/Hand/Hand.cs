using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    private Socket handSocket = null;
    private SteamVR_Behaviour_Pose pose = null;
    private bool pickedUp = false;
    private HandAnimator handAnimator;

    //Stores all interactables in contact with the hand
    public List<Interactable> contactInteractables = new List<Interactable>();

    private void Awake()
    {
        //Initialise socket and pose
        handSocket = GetComponent<Socket>();
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        handAnimator = GetComponentInChildren<HandAnimator>();
    }

    /*OnTrigger enter is called when of other enters the trigger collider 
      (the hand is a trigger, the object is not, so this is called when the object collider enters the hand trigger)*/
    private void OnTriggerEnter(Collider other)
    {
        //Call add interactable if hand enters interactable body
        if (other.gameObject.layer == 8) //Layer 8 is the interactable layer
        {
            this.GetComponent<Collider>().isTrigger = true;
            AddInteractable(other.gameObject);
        }
        else if (other.gameObject.layer == 0)
        {
            this.GetComponent<Collider>().isTrigger = false;
        }
    }

    private void AddInteractable(GameObject newObject)
    {
        //Get the interactable component of the object that the hand is in, add to interactable lists
        Interactable newInteractable = newObject.GetComponent<Interactable>();
        contactInteractables.Add(newInteractable);
    }

    //Opposite of OnTriggerEnter
    private void OnTriggerExit(Collider other)
    {
        //Call remove interactable if hand leaves interactable body
        RemoveInteractable(other.gameObject);
    }

    private void RemoveInteractable(GameObject newObject)
    {
        //Remove interactable hand has just left
        Interactable existingInteractable = newObject.GetComponent<Interactable>();
        contactInteractables.Remove(existingInteractable);
    }

    public void TryInteract()
    {
        Interactable nearest = InteractableDectector.GetNearestInteractable(transform.position, contactInteractables);
        //If there is a nearest interactable (e.g. hand is within the game object body) then interact
        if (nearest && (pickedUp == false || (pickedUp == true && nearest.gameObject.CompareTag("BeltSlot"))))
        {
            nearest.Interact(this);
        }
        else if (pickedUp == true)
        {
           TryDrop();
        }
            
    }

    public void TryAction()
    {
        Grabbable heldObject = handSocket.GetStoredObject();
        /*Can only perform actions on holdable objects. If there is a grabbale object (not null) and it is also holdable, perform the action.
          We check for the held object first as we can't get the component of a null object, otherwise we get errors.*/
        if (heldObject && heldObject.GetComponent<Holdable>())
            heldObject.GetComponent<Holdable>().PerformAction(this);

    }

    private void TryDrop()
    {
        Grabbable heldObject = handSocket.GetStoredObject();

        //Can only drop holdable objects, same reasoning as TryAction()
        if (heldObject && heldObject.GetComponent<Holdable>())
            heldObject.EndInteraction(this);
    }

    public void TryRelease()
    {
       Grabbable heldObject = handSocket.GetStoredObject();

        /*We can only release a squeezable. We need TryRelease() and TryDrop() as seperate functions so that we can map them to seperate controls.
          For example we want to RELEASE a door handle when we stop squeezing it via holding down the trigger. However, we do not want to constantly squeeze 
          the trigger to hold a flashlight as this is unintuitive. We may wish to drop the flashlight by pressing
          the grip buttons, so we must call TryDrop() seperately.*/
       if (heldObject && heldObject.GetComponent<Squeezable>())
           heldObject.EndInteraction(this);
    }

    public Socket GetSocket()
    {
        return handSocket;
    }

    public SteamVR_Behaviour_Pose GetPose()
    {
        return pose;
    }

    public HandAnimator GetHandAnimator()
    {
        return handAnimator;
    }

    public void SetPickedUp(bool givenState)
    {
        pickedUp = givenState;
    }

}
