using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandAnimator : MonoBehaviour
{
    public SteamVR_Action_Single grabAction = null;

    private Animator animator = null;
    private SteamVR_Behaviour_Pose pose = null;
    private bool forceGrab = false;
    private float grabValue = 0.25f;
    private float startZRotation = 0.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        startZRotation = this.transform.localRotation.eulerAngles.y;
        grabAction[pose.inputSource].onChange += Grab;
    }

    private void OnDestroy()
    {
        grabAction[pose.inputSource].onChange -= Grab;
    }

    private void Grab(SteamVR_Action_Single action, SteamVR_Input_Sources source, float axis, float delta)
    {
        if (!forceGrab)
        {
            animator.SetFloat("GrabBlend", axis);
        }
        else {
            animator.SetFloat("GrabBlend", grabValue);
        }
    }

    public void SetForceGrab(bool grab, float grabIntensity)
    {
        forceGrab = grab;
        grabValue = grabIntensity;
    }

    public void SetModelPosition(Vector3 position, Vector3 rotation)
    {
        Vector3 handPosition = this.transform.localPosition;
        Vector3 handAngles = this.transform.localRotation.eulerAngles;
        handAngles.x = rotation.x;
        handPosition.z = position.z;
        if (this.CompareTag("Left Hand"))
        {
            handPosition.x = position.x;
            handAngles.y = rotation.y;
            handAngles.z = rotation.z;
        }
        else
        {
            handPosition.x = -position.x;
            handAngles.y = -rotation.y;
            handAngles.z = -rotation.z;

        }
        this.transform.localPosition = handPosition;
        this.transform.localRotation = Quaternion.Euler(handAngles);
    }
}
