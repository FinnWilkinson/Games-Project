using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Squeezable : Grabbable
{
    protected override void ApplyHandVelocity(Hand hand, Rigidbody rigidBody, float scaleFactor)
    {
        SteamVR_Behaviour_Pose pose = hand.GetPose();
        Vector3 newVelocity = new Vector3(pose.GetVelocity().x, 0, pose.GetVelocity().z);
        rigidBody.velocity = newVelocity * scaleFactor;
    }
}
