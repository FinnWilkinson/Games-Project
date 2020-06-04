using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Door : MonoBehaviour
{
    public bool startLocked;

    private bool locked;
    private HingeJoint hinge;
    private JointLimits limits;
    private Rigidbody rb;

    public void Start()
    {
        locked = startLocked;
        hinge = this.GetComponent<HingeJoint>();
        rb = this.GetComponent<Rigidbody>();
        limits = hinge.limits;

        if (startLocked)
        {
            limits.min = -1.0f;
            limits.max = 1.0f;
            limits.bounciness = 0;
            limits.bounceMinVelocity = 0;
            hinge.limits = limits;
        }
        else
        {
            limits.min = -100f;
            limits.max = 100f;
            limits.bounciness = 0;
            limits.bounceMinVelocity = 0;
            hinge.limits = limits;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand") || other.gameObject.CompareTag("Hand"))
        {
            rb.velocity = other.GetComponent<SteamVR_Behaviour_Pose>().GetVelocity() * 2.0f;
        }
    }

    private void ChangeHinges(float min, float max)
    {
        limits.max = max;
        limits.min = min;
        limits.bounciness = 0.0f;
        limits.bounceMinVelocity = 0.0f;
        hinge.limits = limits;
    }

    public void SetLocked(bool newState)
    {
        locked = newState;
        if(locked)
            ChangeHinges(-1.0f, 1.0f);
        else
            ChangeHinges(-100.0f, 100.0f);
    }

    public bool GetLocked()
    {
        return locked;
    }

}
