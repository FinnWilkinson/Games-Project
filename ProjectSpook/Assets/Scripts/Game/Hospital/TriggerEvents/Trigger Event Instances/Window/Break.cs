using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public GameObject fixedWindow, smashedWindow1, smashedWindow2;
    public float radius, power, upwards;
    public List<Rigidbody> human;
    public AudioClip glassShatter;

    private AudioSource audioSource;

    private void Start()
    {
        fixedWindow.SetActive(true);
        smashedWindow1.SetActive(false);
        smashedWindow2.SetActive(false);
        audioSource = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Human")
        {
            fixedWindow.SetActive(false);
            smashedWindow1.SetActive(true);
            smashedWindow2.SetActive(true);

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

            foreach(Collider hit in colliders)
            {
                if (hit.attachedRigidbody)
                {
                    hit.attachedRigidbody.AddExplosionForce(power * other.attachedRigidbody.velocity.magnitude, explosionPos, radius, 0);
                }
            }
            audioSource.PlayOneShot(glassShatter);

            /*foreach(Rigidbody rb in human)
            {
                rb.velocity = new Vector3(2.0f, 0, 0);
            }*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
         if (other.gameObject.tag == "Human") {
           foreach (Rigidbody rb in human)
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.angularVelocity = new Vector3(0, 0, 0);
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
