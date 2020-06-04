using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPortalTeleporter : MonoBehaviour
{

    public Transform player;
    public Transform playerCamera;
    public Transform reciever;
    public float checkDelay;
    public List<Door> doors;

    private bool playerIsOverlapping = false;
    private bool teleportDone = false;

    private void Start()
    {
        StartCoroutine(CheckOverlap());
    }

    // Update is called once per frame
    private IEnumerator CheckOverlap()
    {
        while (!teleportDone) {
            if (playerIsOverlapping)
            {
                Vector3 portalToPlayer = player.position - transform.position;
                Vector3 newPosition = reciever.position;
                newPosition.y = player.position.y;
                newPosition.z = reciever.position.z + portalToPlayer.z;
                player.position = newPosition;
                playerIsOverlapping = false;
                teleportDone = true;
                
            }
            yield return new WaitForSeconds(checkDelay);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Door d in doors)
            {
                d.SetLocked(false);
            }
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsOverlapping = false;
        }
    }
}
