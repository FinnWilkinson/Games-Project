using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightIndicator : MonoBehaviour
{
    public Flashlight flashlight;
    public Material on, off;
    public float threshold;

    protected float flashlightEnergy;
    protected Material currentMaterial;

    public void Start() 
    {
        StartCoroutine(CheckState());
    }

    private IEnumerator CheckState()
    {
        while (true) {
            yield return new WaitForSeconds(0.1f);
            if (flashlight.GetLightSource().enabled)
            {
                //We get the current energy here only. This avoids needless calls to the getter function
                flashlightEnergy = flashlight.GetCurrentEnergy();
                currentMaterial = this.GetComponent<MeshRenderer>().material;
                CheckBatteryStatus();
            }
            //We check if the material isn't already off, again to avoid needless calls
            else if (currentMaterial != off)
            {
                TurnOff();
            }
        }
    }

    protected virtual void CheckBatteryStatus()
    {
        if (flashlightEnergy <= threshold)
        {
            TurnOff();
        }
        else if (currentMaterial != on)
        {
            TurnOn();
        }
    }

    protected virtual void TurnOff()
    {
        this.GetComponent<MeshRenderer>().material = off;
    }

    protected void TurnOn()
    {
        this.GetComponent<MeshRenderer>().material = on;
    }   

}