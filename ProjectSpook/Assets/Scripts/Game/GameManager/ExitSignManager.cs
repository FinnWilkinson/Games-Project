using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSignManager : MonoBehaviour
{

    public List<Light> exitSigns;

    private Light activeSign;
    private int activeIndex;

    private void Awake()
    {
        foreach (Light exitSign in exitSigns) {
            exitSign.enabled = false;
        }
        activeIndex = 0;
        activeSign = exitSigns[activeIndex];
        activeSign.enabled = true;
    }

    public void UpdateExitSign()
    {
        activeSign.enabled = false;
        ++activeIndex;
        if (activeIndex < exitSigns.Count)
        {
            activeSign = exitSigns[activeIndex];
            activeSign.enabled = true;
        }
        else
        {
            Debug.LogError("Active exit sign index out of bounds. Please ensure to extend the lights list in the game manager");
        }
    }
}