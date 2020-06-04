using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraMatchView : MonoBehaviour
{
    public Transform playerCamera;
    public Transform enterPortal;
    public Transform exitPortal;

    private void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - enterPortal.position;
        transform.position = new Vector3(23.0f, 1.8f, 0.0f);

        transform.rotation = Quaternion.Euler(playerCamera.eulerAngles.x, playerCamera.eulerAngles.y, 0.0f);

    }

}
