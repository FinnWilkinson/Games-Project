using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerEvent : WalkThroughEvent 
{
    public List<Light> allLights;
    public List<Light> shutdownLights;
    public float waitTime;

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitTillTurnOff());
            base.OnTriggerEnter(other);
        } 
    }

    //For explanations of coroutines, invoking and IEnumerator functions, please refer to the flashlight script which invokes Flicker alongside comments

    private IEnumerator WaitTillTurnOff()
    {
        InvokeRepeating("Flicker", 0.5f, 0.5f);
        yield return new WaitForSeconds(waitTime);
        CancelInvoke("Flicker");
        yield return new WaitForSeconds(1.21f);
        SetLightIntensity(Random.Range(0.5f, 1.0f), allLights);
        SetLightIntensity(0.0f, shutdownLights);
    }

    private void Flicker()
    {
        StartCoroutine(WaitFlicker());
    }

    private IEnumerator WaitFlicker()
    {
        SetLightIntensity(Random.Range(0.5f, 1.0f), allLights);
        yield return new WaitForSeconds(Random.Range(0.3f, 0.6f));
        SetLightIntensity(0.0f, allLights);
        yield return new WaitForSeconds(Random.Range(0.3f, 0.6f));
        SetLightIntensity(Random.Range(0.5f, 1.0f), allLights);
    }

    private void SetLightIntensity(float givenIntensity, List<Light> givenLights)
    {
        foreach (Light light in givenLights) {
            light.intensity = givenIntensity;
        }
    }

}
