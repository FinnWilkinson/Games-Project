using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    private Grabbable storedObject = null;
    private FixedJoint joint = null;

    private void Awake()
    {
        joint = GetComponent<FixedJoint>();
    }

    public void Attach(Grabbable newObject)
    {
        //Setting the position and rotation of the stored object
        storedObject = newObject;
        storedObject.transform.position = transform.position;
        storedObject.transform.rotation = storedObject.GetActiveRotation();

        //Attach the rigid body of the object to the fixed joint (socket is essentially logical, but fixed joint is within the game)
        Rigidbody targetBody = storedObject.gameObject.GetComponent<Rigidbody>();
        joint.connectedBody = targetBody;
        
    }

    public void Detach()
    {
        //If we don't have an item attached to the socket, then just return as we can't detach nothing
        if (!storedObject)
        {
            return;
        }

        //Detatch the object from the socket
        joint.connectedBody = null;
        storedObject = null;
    }

    public Grabbable GetStoredObject()
    {
        return storedObject;
    }
}