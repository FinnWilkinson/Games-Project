using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grabbable : Interactable
{
    /*Need an active socket field and active rotation field.
     The rotation of a grabbable depends on it's being grabbed or not*/
    protected Socket activeSocket = null;
    protected Quaternion activeRotation;
    protected Hand attachedHand;

    public override void Interact(Hand hand)
    {
        //We attach grabbables to hand sockets
        AttachNewSocket(hand.GetSocket());
        attachedHand = hand;
    }

    public override void EndInteraction(Hand hand)
    {
        hand.GetHandAnimator().SetForceGrab(false, 0.25f);
        hand.GetHandAnimator().SetModelPosition(new Vector3(0.01f, 0.02f, 0.0f), new Vector3(0.0f, 0.0f, 100.0f));
        ReleaseOldSocket();
    }

    protected void AttachNewSocket(Socket newSocket)
    {
        //To ensure we're releasing from the belt/hand in order to attach to new socket
        ReleaseOldSocket();
        activeSocket = newSocket;

        //Attach grabbable object to socket, set socket to unavaible
        activeSocket.Attach(this);
        this.isAvailable = false;
    }

    private void ReleaseOldSocket()
    {
        //If there is no active socket, can't release
        if (!activeSocket)
            return;

        //Detach body from socket, set active socket to null and make available
        activeSocket.Detach();
        activeSocket = null;
        this.isAvailable = true;
    }

    /*Add physics e.g. throw. We add a scaleFactor as different objects need to be scaled differently
      from the hand velcoity. E.g. heavier vs lighter objects vs slammable doors*/
    protected virtual void ApplyHandVelocity(Hand hand, Rigidbody rigidBody, float scaleFactor)
    {
        SteamVR_Behaviour_Pose pose = hand.GetPose();

        rigidBody.velocity = pose.GetVelocity() * scaleFactor;
        rigidBody.angularVelocity = pose.GetAngularVelocity() * scaleFactor;

    }

    public Quaternion GetActiveRotation()
    {
        return activeRotation;
    }

    public bool InHand()
    {
        return (attachedHand && attachedHand.GetSocket().GetStoredObject()) ? true : false;
    }



}
