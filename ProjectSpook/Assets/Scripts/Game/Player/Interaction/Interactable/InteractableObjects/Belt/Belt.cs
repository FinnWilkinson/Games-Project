using UnityEngine;
using Valve.VR;

public class Belt : MonoBehaviour
{
    //Belt needs to be somewhere between the head and floor, but also able to move up/down based on the head transform
    [Range(0.5f, 0.75f)]
    public float height = 0.5f;

    private Transform head = null;

    private void Start()
    {
        head = SteamVR_Render.Top().head;
    }

    private void Update()
    {
        PositionUnderHead();
        RotateWithHead();
    }

    //Set position of belt under head
    private void PositionUnderHead()
    {
        //Initialise adjusted height to local position of the head
        Vector3 adjustedHeight = head.localPosition;

        //Only want to change y translation, only want to interpolate between to values e.g. 0.0f = feet, adjustedHeight.y = height of player and height = 0.5f is halfway between
        adjustedHeight.y = Mathf.Lerp(0.0f, adjustedHeight.y, height);

        transform.localPosition = adjustedHeight;
    }

    //Belt should only rotate with head in y axis so that it's always underneath them and easy to use
    private void RotateWithHead()
    {
        Vector3 adjustedRotation = head.localEulerAngles;
        adjustedRotation.x = 0;
        adjustedRotation.z = 0;

        transform.localEulerAngles = -adjustedRotation;
    }
}
