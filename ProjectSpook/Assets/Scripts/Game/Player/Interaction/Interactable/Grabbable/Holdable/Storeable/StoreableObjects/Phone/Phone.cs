using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Phone : Storeable
{
    public Material ringingMaterial, idleMaterial, inCallMaterial;
    public AudioClip vibrate, firstCall, endCall;

    public bool isRinging;
    private bool inCall = false;
    private AudioSource audioSource;
    public float targetTime;
    public GameObject textParent;
    private TextMesh textMesh;

    public void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        textMesh = textParent.GetComponentInChildren<TextMesh>();
        StartCoroutine(Ring());
    }

    public override void Interact(Hand hand)
    {
        // These parameters will have to be tuned to the phone

        Vector3 handAngles = new Vector3(hand.transform.eulerAngles.x, hand.transform.eulerAngles.y, hand.transform.eulerAngles.z);
        heldRotation = Quaternion.Euler(handAngles);
        hand.GetHandAnimator().SetForceGrab(true, 0.0f);
        hand.GetHandAnimator().SetModelPosition(new Vector3(-0.05f, 0.02f, -0.015f), new Vector3(10.0f, 75.0f, 187.5f));

        //Call the base start interaction 
        base.Interact(hand);
    }

    public override void PerformAction(Hand hand)
    {
        if (isRinging)
        {
            // Answer or decline the call
            isRinging = false;
        }
        else
        {
            // Show map / controls
        }
    }

    private IEnumerator Ring()
    {
        Material[] materials = new Material[2];
        materials[0] = idleMaterial;
        materials[1] = ringingMaterial;

        this.GetComponent<MeshRenderer>().materials = materials;
        isRinging = true;
        audioSource.loop = true;
        audioSource.PlayOneShot(vibrate);

        while (isRinging) {
            yield return new WaitForSeconds(0.1f);    
        }

        inCall = true;
        audioSource.loop = false;
        audioSource.Stop();
        StartCoroutine(TimerFunction());
    }


    private IEnumerator TimerFunction()
    {
        textMesh.gameObject.SetActive(true);
        Material[] materials = new Material[2];
        materials[0] = idleMaterial;
        materials[1] = inCallMaterial;
        this.GetComponent<MeshRenderer>().materials = materials;
        float time = 0.0f;
        TimeSpan minSecTime = TimeSpan.FromSeconds(time);
        audioSource.PlayOneShot(firstCall);
        targetTime = firstCall.length;
        while (inCall)
        {
            textMesh.text = String.Format("{0}:{1:D2}", minSecTime.Minutes, minSecTime.Seconds);
            time += 1.0f;
            minSecTime = TimeSpan.FromSeconds(time);
            if (time >= targetTime)
            {
                textMesh.text = "Call Ended";
                audioSource.PlayOneShot(endCall);
                yield return new WaitForSeconds(2.0f);
                materials[1] = idleMaterial;
                this.GetComponent<MeshRenderer>().materials = materials;
                inCall = false;
                textMesh.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
