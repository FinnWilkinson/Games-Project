using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardReader : ObjectPlacementEvent
{
    /*this might work wasn't able to test in time*/
    public GameObject attachedDoor;
    public GameObject lockedIndicatorLight, unlockedIndicatorLight;
    public Material lockedMaterial, unlockedMaterial, inactiveMaterial;
    public Rigidbody doorRb;
    //public AudioSource audioSource = null;
    //public AudioClip unlocked, locked, denied;

    public void Start()
    {
        //audioSource = this.GetComponent<AudioSource>();   
    }

    public override void OnTriggerEnter(Collider other)
    {
        /*We need to check that the the collider is a key card.
         * We don't want to run this code if
         * any interactable collides with the reader*/

        if (other.gameObject.CompareTag("Keycard"))
        {
            if(other.gameObject.name == triggerObject.name)
            {
                ChangeDoorState(other);
            }
            else
            {
                StartCoroutine(SignalAccessDenied());   
            }
        }
       
    }

    private void ChangeDoorState(Collider other)
    {
        if (attachedDoor.GetComponent<Door>().GetLocked())
        {
            UnlockDoor();
            Debug.Log("Unlock");
        }
        else
        {
            LockDoor();
            Debug.Log("Lock");
        }
    }

    private void LockDoor()
    {
        //audioSource.clip = locked;
        StartCoroutine(SignalDoorLocked());
        attachedDoor.GetComponent<Door>().SetLocked(true);
        doorRb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void UnlockDoor()
    {
        //audioSource.clip = unlocked;
        StartCoroutine(SignalDoorUnlocked());
        attachedDoor.GetComponent<Door>().SetLocked(false);
        doorRb.constraints = RigidbodyConstraints.None;
    }

    private IEnumerator SignalDoorLocked()
    {
        unlockedIndicatorLight.GetComponent<MeshRenderer>().material = inactiveMaterial;
        lockedIndicatorLight.GetComponent<MeshRenderer>().material = lockedMaterial;
        yield return new WaitForSeconds(2);
        lockedIndicatorLight.GetComponent<MeshRenderer>().material = inactiveMaterial;
    }

    private IEnumerator SignalDoorUnlocked()
    {
        lockedIndicatorLight.GetComponent<MeshRenderer>().material = inactiveMaterial;
        unlockedIndicatorLight.GetComponent<MeshRenderer>().material = unlockedMaterial;
        yield return new WaitForSeconds(1);
        unlockedIndicatorLight.GetComponent<MeshRenderer>().material = inactiveMaterial;
    }

    private IEnumerator SignalAccessDenied()
    {
        lockedIndicatorLight.GetComponent<MeshRenderer>().material = lockedMaterial;
        unlockedIndicatorLight.GetComponent<MeshRenderer>().material = lockedMaterial;
        yield return new WaitForSeconds(2);
        lockedIndicatorLight.GetComponent<MeshRenderer>().material = inactiveMaterial;
        unlockedIndicatorLight.GetComponent<MeshRenderer>().material = inactiveMaterial;
    }

}
