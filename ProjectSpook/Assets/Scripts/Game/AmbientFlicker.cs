using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientFlicker : TriggerEvent
{
    public List<Light> roomLights;
    public AudioClip flickerSound;

    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine("Flicker");
        }
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            foreach (Light light in roomLights)
            {
                if (!light.GetComponent<AudioSource>().isPlaying) {
                    light.GetComponent<AudioSource>().PlayOneShot(flickerSound);
                }
                light.intensity = Random.Range(0.2f, 0.6f);
            }
            yield return new WaitForSeconds(2.0f);
            foreach (Light light in roomLights)
            {
                if (!light.GetComponent<AudioSource>().isPlaying)
                {
                    light.GetComponent<AudioSource>().PlayOneShot(flickerSound);
                }
                light.intensity = 1.0f;
            }
        }
    }
}
