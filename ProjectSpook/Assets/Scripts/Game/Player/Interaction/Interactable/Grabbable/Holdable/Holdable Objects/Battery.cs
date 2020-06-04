using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Holdable
{
    public Flashlight flashlight;
    public AudioClip batterySound;

    public override void Interact(Hand hand)
    {
        base.Interact(hand);  
    }

    public override void PerformAction(Hand hand)
    {
        if (flashlight.IsSocketed())
        {
            flashlight.FullPower();
            flashlight.GetComponent<AudioSource>().clip = batterySound;
            flashlight.GetComponent<AudioSource>().Play();
            this.EndInteraction(hand);
            GetComponent<Battery>().gameObject.SetActive(false);
            Destroy(this);
        }
    }

    public override void EndInteraction(Hand hand)
    {
        base.EndInteraction(hand);
    }
}
