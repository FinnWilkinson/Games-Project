using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Flashlight : Storeable
{
    public Light lightSource;
    public float decayRate;

    private AudioSource audioSource;
    private float maxEnergy;
    private float currentEnergy;
    private readonly float minFlicker = 0.1f;
    private readonly float maxFlicker = 0.2f;

    public void Start()
    {
        //Initialise the variables at the start of the scene
        maxEnergy = 100;
        currentEnergy = maxEnergy;
        lightSource.enabled = false;
        audioSource = this.GetComponent<AudioSource>();
        StartCoroutine(CheckFlashlightState(0.1f));
    }

    IEnumerator CheckFlashlightState(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            //Check if active and decrement the energy and check the battery status 
            if (lightSource.enabled)
            {
                DecrementEnergy();
                CheckBatteryStatus();
            }
        }
    }

    public override void Interact(Hand hand)
    {
        //Get orientation of hand, offset x by -85
        Vector3 handAngles = new Vector3((hand.transform.eulerAngles.x - 90), hand.transform.eulerAngles.y, 0);  
        heldRotation = Quaternion.Euler(handAngles);
        hand.GetHandAnimator().SetForceGrab(true, 0.25f);
        hand.GetHandAnimator().SetModelPosition(new Vector3(0.01f, 0.02f, 0.0f), new Vector3(70.0f, 0.0f, 100.0f));

        //Call the base start interaction 
        base.Interact(hand);
    }

    public override void PerformAction(Hand hand)
    {
        //When interacting with the flashlight, check if it has energy and if so, turn it on/off
        if (currentEnergy > 0)
        {
            SwitchState();
        }
    }

    public override void EndInteraction(Hand hand)
    {
        //Turn off the light and remove from the hand socket
        TurnOff();
        base.EndInteraction(hand);
    }

    public void FullPower()
    {
        //If a battery has been picked up then set the current power to full
        currentEnergy = maxEnergy;
    }

    private void DecrementEnergy()
    {
        currentEnergy -= decayRate * Time.deltaTime;
        //We want the light to dim with the energy. So we set it to the ratio of the currentEnergy and the maxEnergy
        lightSource.intensity = currentEnergy / maxEnergy;

    }

    private void CheckBatteryStatus()
    {
        //We don't want the flashlight to flicker if it has full battery
        if (currentEnergy > 25 && IsInvoking("Flicker"))
        {
            CancelInvoke("Flicker");
        }
        else if (currentEnergy <= 25 && currentEnergy > 0 && !IsInvoking("Flicker"))
        {
            //This invokes the flicker function. It means wait for 0.5 seconds before calling Flicker and then repeat the call every 1.5 seconds.
            InvokeRepeating("Flicker", 0.5f, 1.5f);
        }
        else if (currentEnergy < 0)
        {
            OutOfEnergy();
        }
        
    }

    private void SwitchState()
    {
        lightSource.enabled = !lightSource.enabled;
        audioSource.PlayOneShot(audioSource.clip);
    }

    private void TurnOff()
    {
        //Stop the flashlight from flickering
        CancelInvoke("Flicker");
        lightSource.intensity = 0;
        lightSource.enabled = false;
    }

    private void OutOfEnergy()
    {
        TurnOff();
        currentEnergy = 0;
    }

    private void Flicker()
    {
        //For timed events we need a coroutine which yields a return based on elapsed time
        StartCoroutine(WaitFlicker());
    }

    //This is the timed function to cause a flicker
    private IEnumerator WaitFlicker()
    {
        lightSource.intensity = 0;

        /*This line essentialyl says "Wait for a random time between the min and max flicker time (0.1 - 0.2 seconds in this case).
          Once this time has passed, return to me and I'll execute the rest of my code." Timed events have to be IEnumerators
          apparently.*/
        yield return new WaitForSeconds(Random.Range(minFlicker, maxFlicker));

        lightSource.intensity = 0.25f;
        yield return new WaitForSeconds(Random.Range(minFlicker, maxFlicker));
        lightSource.intensity = 0;
        yield return new WaitForSeconds(Random.Range(minFlicker, maxFlicker));
        lightSource.intensity = 0.25f;
    }

    public float GetCurrentEnergy()
    {
        return currentEnergy;
    }

    public Light GetLightSource()
    {
        return lightSource;
    }

}
