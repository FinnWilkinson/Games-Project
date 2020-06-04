﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreFlashlight : Objective
{
    public Flashlight flashlight;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CheckCompletion", 0.05f);
    }

    IEnumerator CheckCompletion(float delay)
    {
        while (!completed)
        {
            yield return new WaitForSeconds(delay);
            if (flashlight.IsStored())
            {
                completed = true;
            }
        }

    }
}
