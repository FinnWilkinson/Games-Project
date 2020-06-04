using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class Handle : Squeezable
{
    /*This class is a logical handle that we can interact with. The parent is the physical handle
      that's attached to the door within Unity. We need this seperation so that we don't pull handles
      off the door when we try to interact with them.*/
    private Transform parent;
    private Rigidbody door;

    public void Start()
    {
        parent = this.transform.parent;
        door = parent.transform.parent.GetComponent<Rigidbody>();
    }

    private IEnumerator CheckReposition()
    {
        yield return new WaitForSeconds(0.1f);
        if (attachedHand && Vector3.Distance(this.transform.position, parent.position) > 0.05f)
        {
            EndInteraction(attachedHand);
        }
    }

    public override void Interact(Hand hand)
    {
        if (!hand.GetSocket().GetStoredObject())
        {
            hand.GetHandAnimator().SetForceGrab(true, 0.25f);
            hand.GetHandAnimator().SetModelPosition(new Vector3(0.0f, 0.0f, -0.1f), new Vector3(0.0f, 0.0f, 100.0f));
            //The physical handle (parent) now follows the logical one
            parent.GetComponent<FollowPhysics>().enabled = true;
            this.transform.GetComponent<FollowPhysics>().enabled = false;
            //Attach to hand
            base.Interact(hand);
            //Remember the attached hand
            attachedHand = hand;
        }

        StartCoroutine(CheckReposition());
    }

    public override void EndInteraction(Hand hand)
    {

        //Reposition it back to the physical handle
        RepositionHandle(hand);
        //Release the logical handle from the hand
        base.EndInteraction(hand);
    }

    private void RepositionHandle(Hand hand)
    {
        /*We need to turn on kinematics for the door and the physical handle before we reposition the logical handle. 
          Turning on kinematics essentially stops the RigidBody behaving according to physics.
          We do this because when the physical handle is trying to follow the RigidBody of the logical handle, 
          it gets some fixed, "stored" velocity. This means that when we release the logical handle from the hand, 
          even if the door is stationary at first, it will jolt forward due to the stored
          velocity. We can't just set the velocity to 0 as there are colliders
          which will cause the door and handle to move again.
          By turning on kinematic's we ignore this velocity and get a more realistic behaviour.*/
        door.isKinematic = true;
        parent.transform.GetComponent<Rigidbody>().isKinematic = true;

        //The logical handle now follows the physical handle. This is because the logical one is realeased from the hand
        parent.GetComponent<FollowPhysics>().enabled = false;
        this.GetComponent<FollowPhysics>().enabled = true;
        this.transform.localRotation = Quaternion.Euler(Vector3.zero);
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        //We turn kinematics back on so that the door and handle behave according to physics once again
        door.isKinematic = false;
        parent.transform.GetComponent<Rigidbody>().isKinematic = false;

        //Apply some hand velocity so that doors can slam open and shut based on how hard we close them
        ApplyHandVelocity(hand, door, 8.0f);
    }

}
