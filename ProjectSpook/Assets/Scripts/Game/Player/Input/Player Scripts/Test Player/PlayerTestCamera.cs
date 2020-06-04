using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestCamera : MonoBehaviour
{
    public float speed;
    public bool teleport;

    private Vector2 mouseDirection;
    private Quaternion storedRotation;
    private Vector3 storedEuler;


    void Update()
    {
        Vector2 cursorPosition = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        if (teleport)
        {
            storedEuler = storedRotation.eulerAngles;
            storedEuler.y += 90.0f;
            this.transform.rotation = Quaternion.Euler(storedEuler.x, storedEuler.y, storedEuler.z);
            mouseDirection = new Vector2(storedEuler.y, -storedEuler.x);
        }
        else {
            mouseDirection += cursorPosition;
            this.transform.rotation = Quaternion.Euler(-mouseDirection.y, mouseDirection.x, 0.0f);

        }
    }

    public void SetTeleport(bool tele, Quaternion lastRotation) {
        teleport = tele;
        storedRotation = lastRotation;
    }

}
