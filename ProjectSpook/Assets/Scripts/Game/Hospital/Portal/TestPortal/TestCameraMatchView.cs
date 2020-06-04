using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraMatchView : MonoBehaviour
{
    public Transform playerCamera;
    public Transform enterPortal;
    public Transform exitPortal;
    private void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - enterPortal.position;
        transform.position = new Vector3(22.1f, playerCamera.position.y, playerOffsetFromPortal.z);

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(enterPortal.rotation, exitPortal.rotation);

        Vector3 newCameraDirection = playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

    }

}
