using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPortalTeleporter : TriggerEvent
{

    public Transform player;
    public Transform reciever;
    public float checkDelay;
    public List<Door> doors;

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Door d in doors)
            {
                d.SetLocked(false);
            }
            Vector3 positionOffset = player.position - transform.position;

            Vector3 newPostition = reciever.position;
            newPostition.y = player.position.y;
            newPostition.z = reciever.position.z + positionOffset.z;
            player.position = newPostition;
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.enabled = false;
        }
    }

}
