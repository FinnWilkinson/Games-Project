using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestControls : MonoBehaviour
{
    public float speed;
    public Camera cam;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        Vector3 rotationVector = new Vector3(0.0f, cam.transform.rotation.eulerAngles.y, 0.0f);
        transform.rotation = Quaternion.Euler(rotationVector);
        transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime*speed, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed);
        if (Input.GetKeyDown(KeyCode.F)) {
            this.GetComponentInChildren<Light>().enabled = !this.GetComponentInChildren<Light>().enabled;
        }
    }

}
