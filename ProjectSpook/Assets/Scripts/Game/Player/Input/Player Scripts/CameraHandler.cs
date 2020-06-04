using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CameraHandler : MonoBehaviour
{
    public CharacterController charCont;
    public SteamVR_Action_Vibration hapticAction;

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.layer == 12) { // Layer 12 is the door layer
            // Calculate Angle Between the collision point and the player
            Vector3 dir = c.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            charCont.Move(0.2f * dir);
        }
    }

    private void OnCollisionExit(Collision c)
    {
        if (!c.gameObject.CompareTag("Player")){
            Pulse(0.1f, 150.0f, 0.1f, SteamVR_Input_Sources.LeftHand);
            Pulse(0.1f, 150.0f, 0.1f, SteamVR_Input_Sources.RightHand);
        }
    }

    private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        hapticAction.Execute(0, duration, frequency, amplitude, source);
    }

}
