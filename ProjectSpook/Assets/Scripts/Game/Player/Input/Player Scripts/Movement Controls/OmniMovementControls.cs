using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class OmniMovementControls : MonoBehaviour
{
    public SteamVR_Action_Vector2 touchpadDirection;
    public Transform cameraRig;
    public Transform head;
    public float sensitivity = 0.1f;
    public float gravity = 50.0f;

    private CharacterController charCont;
    private float currentSpeed = 0.0f;
    private float rotationOffset = 0.0f;

    private void Awake()
    {
        charCont = GetComponent<CharacterController>();
    }

    private void Start()
    {
        cameraRig = SteamVR_Render.Top().origin;
        head = SteamVR_Render.Top().head;
        rotationOffset = this.transform.rotation.eulerAngles.y;
    }

    private void Update()
    {
        HandleHead();
        HandleHeight();
        CalculateMovement();

    }

    private void HandleHead()
    {   
        //Store current position
        Vector3 oldPosition = cameraRig.position;
        Quaternion oldRotation = cameraRig.rotation;

        //Rotation
        transform.eulerAngles = new Vector3(0.0f, head.rotation.eulerAngles.y + rotationOffset, 0.0f);

        //Restore
        cameraRig.position = oldPosition;
        cameraRig.rotation = oldRotation;
    }

    private void HandleHeight()
    {
        //Get the head in local space
        float headHeight = Mathf.Clamp(head.localPosition.y, 0.5f, 2.0f);
        charCont.height = headHeight;

        //Cut in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = charCont.height / 2;
        newCenter.y += charCont.skinWidth;


        //This section stops the camera from moving inside of objects

        //Move capsule in local space
        newCenter.x = head.localPosition.x;
        newCenter.z = head.localPosition.z;

        ////Rotate
        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y + rotationOffset, 0) * newCenter;

        //Section over

        //Apply
        charCont.center = newCenter;
    }

    private void CalculateMovement()
    {
        // Movement orienatation
        Quaternion orientation = CalculateOrientation();
        Vector3 movement = Vector3.zero;

        // Not moving?
        if (touchpadDirection.axis.magnitude == 0)
        {
            currentSpeed = 0;
        }

        // Add and Clamp
        currentSpeed += touchpadDirection.axis.magnitude * sensitivity;

        // Clamp between some maximum speed
        currentSpeed = Mathf.Clamp(currentSpeed, -1.25f, 1.25f);
       

        // Orientation
        movement += orientation * (currentSpeed * Vector3.forward);

        // Gravity
        if (!charCont.isGrounded)
            movement.y -= gravity * Time.deltaTime;

        // Apply
        charCont.Move(movement * Time.deltaTime);
    }

    private Quaternion CalculateOrientation()
    {
        // This allows for motion in directions based on the trackpad
        float rotation = Mathf.Atan2(touchpadDirection.axis.x, touchpadDirection.axis.y);
        rotation *= Mathf.Rad2Deg;
        Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y + rotation - rotationOffset, 0);

        return (Quaternion.Euler(orientationEuler));
    }

}