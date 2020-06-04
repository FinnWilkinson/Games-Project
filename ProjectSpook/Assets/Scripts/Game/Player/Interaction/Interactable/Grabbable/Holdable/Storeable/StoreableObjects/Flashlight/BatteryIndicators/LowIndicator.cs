using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowIndicator : FlashlightIndicator
{

    public Material lowBattery;
    private bool flashOn = false;

    /*The "low battery" indicator check's the battery slightly differently than a normal indicator. We overrid the virtual function and
     include the extra condition as needed.*/
    protected override void CheckBatteryStatus()
    {
        
        if (flashlightEnergy <= threshold && flashlightEnergy > 0 && !IsInvoking("Flash"))
        {
            //This invokes the function "Flash()" immediately (0 seconds) and repeats every 0.1 seconds
            InvokeRepeating("Flash", 0.0f, 0.1f);
        }
        else if (flashlightEnergy < 0)
        {
            TurnOff();
        }
        else if (flashlightEnergy > threshold)
        {
            if (IsInvoking("Flash"))
                CancelInvoke("Flash");

            if (currentMaterial != on)
                TurnOn();
        }
    }

    protected override void TurnOff()
    {
        if (IsInvoking("Flash"))
            CancelInvoke("Flash");
        base.TurnOff();
    }

    private void Flash()
    {
        
        if (flashOn == true)
        {
            this.GetComponent<MeshRenderer>().material = off;
            flashOn = false;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material = lowBattery;
            flashOn = true;
        }
    }
}
